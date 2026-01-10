using System.Text.Json;
using WinTrek.Core.Enums;
using WinTrek.Core.Models;

namespace WinTrek.Core.Services;

/// <summary>
/// Handles saving and loading game state.
/// </summary>
public class GameSaveService
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    /// <summary>
    /// Creates save data from the current game state.
    /// </summary>
    public GameSaveData CreateSaveData(
        GameState state,
        Enterprise enterprise,
        Galaxy galaxy,
        IReadOnlyList<KlingonShip> klingons)
    {
        var saveData = new GameSaveData
        {
            SavedAt = DateTime.Now,
            Stardate = state.Stardate,
            TimeRemaining = state.TimeRemaining,
            IsGameActive = state.IsGameActive,
            Enterprise = CreateEnterpriseSaveData(enterprise),
            Galaxy = CreateGalaxySaveData(galaxy)
        };

        // Save current quadrant Klingons
        foreach (var klingon in klingons)
        {
            saveData.CurrentKlingons.Add(new KlingonSaveData
            {
                SectorX = klingon.SectorPosition.X,
                SectorY = klingon.SectorPosition.Y,
                ShieldLevel = klingon.ShieldLevel
            });
        }

        return saveData;
    }

    /// <summary>
    /// Saves game data to a JSON file.
    /// </summary>
    public void SaveToFile(GameSaveData saveData, string filePath)
    {
        var json = JsonSerializer.Serialize(saveData, JsonOptions);
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Loads game data from a JSON file.
    /// </summary>
    public GameSaveData? LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            return null;

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<GameSaveData>(json, JsonOptions);
    }

    /// <summary>
    /// Restores game state from save data.
    /// </summary>
    public (GameState state, Enterprise enterprise, Galaxy galaxy, List<KlingonShip> klingons) RestoreFromSaveData(GameSaveData saveData)
    {
        var state = new GameState
        {
            Stardate = saveData.Stardate,
            TimeRemaining = saveData.TimeRemaining,
            IsGameActive = saveData.IsGameActive
        };

        var enterprise = RestoreEnterprise(saveData.Enterprise);
        var galaxy = RestoreGalaxy(saveData.Galaxy);
        var klingons = RestoreKlingons(saveData.CurrentKlingons);

        return (state, enterprise, galaxy, klingons);
    }

    private EnterpriseSaveData CreateEnterpriseSaveData(Enterprise enterprise)
    {
        var data = new EnterpriseSaveData
        {
            QuadrantX = enterprise.QuadrantPosition.X,
            QuadrantY = enterprise.QuadrantPosition.Y,
            SectorX = enterprise.SectorPosition.X,
            SectorY = enterprise.SectorPosition.Y,
            Energy = enterprise.Energy,
            ShieldLevel = enterprise.ShieldLevel,
            PhotonTorpedoes = enterprise.PhotonTorpedoes,
            IsDocked = enterprise.IsDocked
        };

        // Save damage state
        foreach (ShipSystem system in Enum.GetValues<ShipSystem>())
        {
            data.DamageState[system.ToString()] = enterprise.Damage.GetDamageLevel(system);
        }

        return data;
    }

    private GalaxySaveData CreateGalaxySaveData(Galaxy galaxy)
    {
        var data = new GalaxySaveData
        {
            TotalKlingons = galaxy.TotalKlingons,
            TotalStarbases = galaxy.TotalStarbases
        };

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                var quadrant = galaxy[x, y];
                var quadrantData = new QuadrantSaveData
                {
                    X = x,
                    Y = y,
                    Name = quadrant.Name,
                    KlingonCount = quadrant.KlingonCount,
                    HasStarbase = quadrant.HasStarbase,
                    StarCount = quadrant.StarCount,
                    IsScanned = quadrant.IsScanned,
                    StarbaseX = -1,
                    StarbaseY = -1
                };

                // Save star and starbase positions
                for (int sy = 0; sy < 8; sy++)
                {
                    for (int sx = 0; sx < 8; sx++)
                    {
                        var content = quadrant.Sectors[sy, sx];
                        if (content == SectorContent.Star)
                        {
                            quadrantData.StarPositions.Add(new[] { sx, sy });
                        }
                        else if (content == SectorContent.Starbase)
                        {
                            quadrantData.StarbaseX = sx;
                            quadrantData.StarbaseY = sy;
                        }
                    }
                }

                data.Quadrants.Add(quadrantData);
            }
        }

        return data;
    }

    private Enterprise RestoreEnterprise(EnterpriseSaveData data)
    {
        var enterprise = new Enterprise
        {
            QuadrantPosition = new Position(data.QuadrantX, data.QuadrantY),
            SectorPosition = new Position(data.SectorX, data.SectorY),
            Energy = data.Energy,
            ShieldLevel = data.ShieldLevel,
            PhotonTorpedoes = data.PhotonTorpedoes,
            IsDocked = data.IsDocked
        };

        // Restore damage
        foreach (var kvp in data.DamageState)
        {
            if (Enum.TryParse<ShipSystem>(kvp.Key, out var system))
            {
                enterprise.Damage.SetDamageLevel(system, kvp.Value);
            }
        }

        return enterprise;
    }

    private Galaxy RestoreGalaxy(GalaxySaveData data)
    {
        var galaxy = new Galaxy();

        // We need to set up quadrants from save data
        foreach (var qData in data.Quadrants)
        {
            var quadrant = galaxy[qData.X, qData.Y];
            quadrant.IsScanned = qData.IsScanned;

            // Clear the quadrant sectors first
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    quadrant.Sectors[y, x] = SectorContent.Empty;
                }
            }

            // Restore starbase
            if (qData.HasStarbase && qData.StarbaseX >= 0 && qData.StarbaseY >= 0)
            {
                quadrant.Sectors[qData.StarbaseY, qData.StarbaseX] = SectorContent.Starbase;
            }

            // Restore stars
            foreach (var starPos in qData.StarPositions)
            {
                if (starPos.Length >= 2)
                {
                    quadrant.Sectors[starPos[1], starPos[0]] = SectorContent.Star;
                }
            }
        }

        return galaxy;
    }

    private List<KlingonShip> RestoreKlingons(List<KlingonSaveData> klingonData)
    {
        var klingons = new List<KlingonShip>();

        foreach (var data in klingonData)
        {
            klingons.Add(new KlingonShip
            {
                SectorPosition = new Position(data.SectorX, data.SectorY),
                ShieldLevel = data.ShieldLevel
            });
        }

        return klingons;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridInfo
{
    public readonly static int chunkLength = 16; // Chunkens bredd mätt i celler måste vara ett jämnt tal
    public readonly static int chunkHeight = 32;

    public readonly static int regionLength = 8; // Regionens bredd mätt i chunks

    public static readonly Vector3Int cellDimensions = Vector3Int.one;
    public static readonly Vector3 chunkDimensions = new Vector3(chunkLength, chunkHeight, chunkLength);

    public static Chunk[,] chunks;

    public static Dictionary<Vector3, Chunk> loadedChunks = new Dictionary<Vector3, Chunk>(); // Dictionaryn håller koll på laddade chunks

    public static void GenerateCells(Chunk _chunk)
    {
        Cell[,,] cells = new Cell[chunkLength, chunkHeight, chunkLength];

        float xOffset = _chunk.position.x - (chunkLength / 2); // Centrerar cellerna
        float zOffset = _chunk.position.z - (chunkLength / 2);


        for (int y = 0; y < chunkHeight; y++)
        {
            for (int x = 0; x < chunkLength; x++)
            {
                for (int z = 0; z < chunkLength; z++)
                {
                    cells[x, y, z] = new Cell(new Vector3Int(x + (int)xOffset, y, z + (int)zOffset)); // Initzialerar chunksen beroende på den tidagare kalkylerade offseten
                }
            }
        }

        _chunk.cells = cells;
    }

    public static Chunk[,] GenerateChunks()
    {
        Chunk[,] chunks = new Chunk[regionLength, regionLength];

        float xOffset = regionLength * chunkLength / 2 - chunkLength / 2; // Centrerar chunksen med +-0.5 skillnad
        float zOffset = regionLength * chunkLength / 2 - chunkLength / 2;
        xOffset = chunkLength % 2 == 0 ? xOffset += 0.5f : xOffset; // Tar bort skillnaden beroende op om talet är jämnt eller ej
        zOffset = chunkLength % 2 == 0 ? zOffset += 0.5f : zOffset;

        float yOffset = chunkHeight % 2 == 0 ? -0.5f : 0;

        for (int x = 0; x < regionLength; x++)
        {
            for (int z = 0; z < regionLength; z++)
            {
                chunks[x, z] = new Chunk(new Vector3(x * chunkLength - xOffset, chunkHeight / 2 + yOffset, z * chunkLength - zOffset), new Vector3Int(x, 0, z));
            }
        }

        return chunks;
    }

    public static Cell GetCell(Vector3 _point)
    {
        Chunk _chunk = GetChunk(_point);
        if (_chunk == null) return null;

        for (int x = 0; x < chunkLength; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                for (int z = 0; z < chunkLength; z++)
                {
                    if (_chunk.cells[x, y, z].position == _point) return _chunk.cells[x, y, z];
                }
            }
        }
 
        return null;
    }

    public static Chunk GetChunk(Vector3 _point)
    {
        for (int x = 0; x < regionLength; x++)
        {
            for (int z = 0; z < regionLength; z++)
            {
                if (ChunkContainsPoint(_point, chunks[x, z])) return chunks[x, z];
            }
        }

        return null;
    }

    public static bool ChunkContainsPoint(Vector3 _point, Chunk _chunk)
    {
        if (_point.x > _chunk.position.x - (chunkLength / 2) && _point.x < _chunk.position.x + (chunkLength / 2)) // De två if statementsen kollar om punkten är innanför eller utanför chunken
        {
            if (_point.z > _chunk.position.z - (chunkLength / 2) && _point.z < _chunk.position.z + (chunkLength / 2))
            {
                return true;
            }
        }

        return false;
    }

    public static Chunk[] GetNeighbouringChunks(Vector3 _point)
    {
        Chunk chunk = GetChunk(_point);
        if (chunk == null) return null;

        Vector3Int origin = chunk.coordinates;

        Chunk[] sorroundingChunks = new Chunk[]{ // Snälla ifrågasätt inte den här koden, den funkar perfekt (det nullar chunken om den inte finns/ är out of bounds) det ser bara väldigt fuckat ut lol
            origin.x - 1 >= 0 && origin.z - 1 >= 0 ? chunks[origin.x - 1, origin.z - 1] : null,
            origin.z - 1 >= 0? chunks[origin.x, origin.z - 1] : null,
            origin.x + 1 < regionLength && origin.z - 1 >= 0? chunks[origin.x + 1, origin.z - 1] : null,
            origin.x - 1 >= 0 ? chunks[origin.x - 1, origin.z] : null, 
            origin.x + 1 < regionLength ? chunks[origin.x + 1, origin.z] : null,
            origin.x - 1 >= 0 && origin.z + 1 < regionLength ? chunks[origin.x - 1, origin.z + 1] : null,
            origin.z + 1 < regionLength ? chunks[origin.x, origin.z + 1] : null,
            origin.x + 1 < regionLength && origin.z + 1 < regionLength ? chunks[origin.x + 1, origin.z + 1] : null,
            origin.x < regionLength && origin.x >= 0 && origin.z < regionLength && origin.z >= 0 ? chunks[origin.x, origin.z] : null
        };

        return sorroundingChunks;
    }
    public static Chunk[] GetNeighbouringChunksInclusive(Vector3 _point)
    {
        Chunk chunk = GetChunk(_point);
        if (chunk == null) return null;

        Vector3Int origin = chunk.coordinates;

        Chunk[] sorroundingChunks = new Chunk[]{ // Snälla ifrågasätt inte den här koden, den funkar perfekt (det nullar chunken om den inte finns/ är out of bounds) det ser bara väldigt fuckat ut lol
            origin.x - 1 >= 0 && origin.z - 1 >= 0 ? chunks[origin.x - 1, origin.z - 1] : null,
            origin.z - 1 >= 0 ? chunks[origin.x, origin.z - 1] : null,
            origin.x + 1 < regionLength && origin.z - 1 >= 0? chunks[origin.x + 1, origin.z - 1] : null,
            origin.x - 1 >= 0 ? chunks[origin.x - 1, origin.z] : null,
            origin.x + 1 < regionLength ? chunks[origin.x + 1, origin.z] : null,
            origin.x - 1 >= 0 && origin.z + 1 < regionLength ? chunks[origin.x - 1, origin.z + 1] : null,
            origin.z + 1 < regionLength ? chunks[origin.x, origin.z + 1] : null,
            origin.x + 1 < regionLength && origin.z + 1 < regionLength ? chunks[origin.x + 1, origin.z + 1] : null,
            origin.x < regionLength && origin.x >= 0 && origin.z < regionLength && origin.z >= 0 ? chunks[origin.x, origin.z] : null,
            GetChunk(GameManager.player.transform.position)
        };

        return sorroundingChunks;
    }

    public static Vector3Int PointToGrid(Vector3 _point)
    {
        int pointX = Mathf.RoundToInt(_point.x);
        int pointY = Mathf.RoundToInt(_point.y);
        int pointZ = Mathf.RoundToInt(_point.z);

        return new Vector3Int(pointX, pointY, pointZ);
    }

    public class Chunk
    {
        public readonly Vector3 position = Vector3.zero;
        public readonly Vector3 dimensions = chunkDimensions;

        public readonly Vector3Int coordinates = Vector3Int.zero;

        public Cell[,,] cells = new Cell[chunkHeight, chunkLength, chunkLength];
        public List<Cell> activeCells = new List<Cell>();

        public Chunk(Vector3 _position, Vector3Int _coordinates)
        {
            position = _position;

            coordinates = _coordinates;
        }
    }

    public static class CellStates
    {
        public enum OccupationState { empty, occupied, blocked }
    }

    public class Cell
    {
        public readonly Vector3Int position = Vector3Int.zero;
        public readonly Vector3Int dimensions = cellDimensions;

        public StructureObject structure = null;
        public GameObject gameObject;


        public CellStates.OccupationState occupationState = CellStates.OccupationState.empty;

        public Cell(Vector3Int _position)
        {
            position = _position;
        }
    }
}

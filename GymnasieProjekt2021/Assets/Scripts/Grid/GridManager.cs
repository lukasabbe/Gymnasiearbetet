using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager{
    public static List<Chunk> chunks = new List<Chunk>();
    public static int gridLength, gridHeight, chunkLength;
    public static void InitializeChunks(int chunkLenght, int length, int height){
        for (int x = 0; x < chunkLenght; x++){
            for (int z = 0; z < chunkLenght; z++){
                float yOffset = 0;
                float xzOffset = chunkLenght / 2 * length;
                if (height % 2 == 0) yOffset = -0.5f;
               
                Vector3 chunkPosition = new Vector3(x * length - 0.5f - xzOffset, height / 2 + yOffset, z * length - 0.5f - xzOffset);
                Vector3 chunkDimensions = new Vector3(length, height, length);
                /*
               Chunk chunk = new Chunk(InitializeCells(length, height, x, z, offset), chunkPosition, chunkDimensions, chunks.Count);
               chunks.Add(chunk)
               */
                Chunk chunk = new Chunk(null, chunkPosition, chunkDimensions, chunks.Count);
                chunks.Add(chunk);
            }
        }
    }
    /*
    public static List<Cell> InitializeCells(int length, int height, int iterationX, int iterationZ, float offset){
        List<Cell> cells = new List<Cell>();

        for (int x = 0; x < length; x++){
            for (int y = 0; y < height; y++){
                for (int z = 0; z < length; z++){
                    Cell newCell = new Cell(new Vector3(x - (length / 2) + (iterationX * length) - offset, y, z - (length / 2) + (iterationZ * length) - offset));
                    cells.Add(newCell);
                }
            }
        }
        return cells;
    }
    */
    public static List<Cell> InitializeCells(Chunk chunk){
        List<Cell> cells = new List<Cell>();

        Vector3 origin = chunk.position;

        int length = (int)chunk.size.x; //Mängden celler längs sidorna
        int height = (int)chunk.size.y; 

        for (int y = 0; y < height; y++){
            for (int x = 0; x < length; x++){
                for (int z = 0; z < length; z++){
                    Cell newCell = new Cell(new Vector3(x + origin.x - (gridLength / 2) + 0.5f, y, z + origin.z - (gridLength / 2) + 0.5f));
                    cells.Add(newCell);
                }
            }
        }
        return cells;
    }
    public static void LoadChunk(Chunk chunk){
        if (chunk.isLoaded) return;
        chunk.cells = InitializeCells(chunk);
        chunk.isLoaded = true;

    }
    public static void LoadChunks(Chunk[] chunks){
        for (int i = 0; i < chunks.Length; i++){
            if (!chunks[i].isLoaded){
                chunks[i].cells = InitializeCells(chunks[i]);
                chunks[i].isLoaded = true;
            }
        }
    }
    public static void BuildStructure(Cell cell, StructureObject structure){
        if (structure != null && !cell.occupied){
            cell.occupied = true;

            cell.structure = structure;        
            cell.gameobject = GameObject.Instantiate(cell.structure.gameObject, cell.position, Quaternion.identity);
        }
    }
    public static void RemoveStructure(Cell cell){
        if (cell.occupied){
            cell.occupied = false;

            GameObject.Destroy(cell.gameobject);
            cell.structure = null;
            cell.gameobject = null;
        }
    }
    public class Cell{ 
        public bool occupied = false;
        public StructureObject structure = null;
        public GameObject gameobject = null;

        public Vector3 position = Vector3.zero;
        public Cell(Vector3 _position){
            position = _position;
        }
    }
    public class Chunk{
        public bool isLoaded = false;

        public List<Cell> cells = new List<Cell>();

        public Vector3 position = Vector3.zero;
        public Vector3 size = Vector3.zero;
        public Collider collider = new Collider();

        public int index = -1;

        public Chunk(List<Cell> _cells, Vector3 _position, Vector3 _size, int _index){
            cells = _cells;
            position = _position;
            size = _size;
            index = _index;
        }
    }
    public static Cell GetCell(Vector3 position, Chunk chunk){
        int iteration = -1;
        for (int i = 0; i < chunk.cells.Count; i++){
            if (chunk.cells[i].position == position)
                iteration = i;
        }
        if (iteration == -1) return null;
        else return chunk.cells[iteration];
    }
    public static Chunk GetChunk(Vector3 position){
        int iteration = -1;
        for (int i = 0; i < chunks.Count; i++){
            if (position.x > chunks[i].position.x - (chunks[i].size.x / 2) && position.x < chunks[i].position.x + (chunks[i].size.x / 2)){
                if (position.z > chunks[i].position.z - (chunks[i].size.z / 2) && position.z < chunks[i].position.z + (chunks[i].size.z / 2)){
                    iteration = i;
                }
            }
        }
        if (iteration == -1) return null;
        else return chunks[iteration];
    }
    public static Chunk GetChunk(int index){
        for (int i = 0; i < chunks.Count; i++)
            if (chunks[i].index == index) return chunks[i];

       return null;
    }
    public static Chunk[] GetSorroundingChunks(Vector3 position){
        int origin = GetChunk(position).index;
        Chunk[] sorroundingChunks = new Chunk[]{
            GetChunk(origin - (chunkLength - 1)),
            GetChunk(origin - chunkLength),
            GetChunk(origin - (chunkLength + 1)),
            GetChunk(origin - 1),
            GetChunk(origin + 1),
            GetChunk(origin + (chunkLength - 1)),
            GetChunk(origin + chunkLength),
            GetChunk(origin + (chunkLength + 1))
        };
        return sorroundingChunks;
    }
}



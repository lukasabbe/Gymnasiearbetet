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
    public static List<Cell> InitializeCells(Chunk chunk){
        List<Cell> cells = new List<Cell>();

        Vector3 origin = chunk.position;

        int length = (int)chunk.size.x; //Mängden celler längs sidorna
        int height = (int)chunk.size.y; 

        for (int y = 0; y < height; y++){
            for (int x = 0; x < length; x++){
                for (int z = 0; z < length; z++){
                    Cell newCell = new Cell(new Vector3(x + origin.x - (gridLength / 2) + 0.5f, y, z + origin.z - (gridLength / 2) + 0.5f), cells.Count);
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
    public static void LoadChunks(List<Chunk> chunks){
        for (int i = 0; i < chunks.Count; i++){
            if (!chunks[i].isLoaded){
                chunks[i].cells = InitializeCells(chunks[i]);
                chunks[i].isLoaded = true;
            }
        }
    }
    public static void BuildStructure(Cell cell, StructureObject structure){
        if (structure != null && !cell.isOccupied){
            cell.isOccupied = true;

            cell.structure = structure;        
            cell.gameobject = GameObject.Instantiate(cell.structure.gameObject, cell.position, Quaternion.identity);
        }
    }
    public static void RemoveStructure(Cell cell){
        if (cell.isOccupied){
            cell.isOccupied = false;

            GameObject.Destroy(cell.gameobject);
            cell.structure = null;
            cell.gameobject = null;
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
    public static Cell GetCell(int index, Chunk chunk){
        for (int i = 0; i < chunk.cells.Count; i++)
            if (chunk.cells[i].index == index) return chunk.cells[i];

        return null;
    }
    public static List<Cell> GetSorroundingCellsHorizontal(Vector3 position){
        Chunk chunk = GetChunk(position);

        int origin = GetCell(position, chunk).index;

        List<Cell> sorroundingCells = new List<Cell>();
        Cell cellToAdd = null;

        bool isValidCell = false;

        for (int i = 0; i < 8; i++)
        {
            isValidCell = true;
            switch (i)
            {
                case (0):
                    cellToAdd = GetCell(origin - (gridLength - 1), chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
                case (1):
                    cellToAdd = GetCell(origin - gridLength, chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
                case (2):
                    cellToAdd = GetCell(origin - (gridLength + 1), chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
                case (3):
                    cellToAdd = GetCell(origin - 1, chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
                case (4):
                    cellToAdd = GetCell(origin + 1, chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
                case (5):
                    cellToAdd = GetCell(origin + (gridLength - 1), chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
                case (6):
                    cellToAdd = GetCell(origin + gridLength, chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
                case (7):
                    cellToAdd = GetCell(origin + (gridLength + 1), chunk);
                    if (cellToAdd == null) isValidCell = false;
                    break;
            }
            if (isValidCell) sorroundingCells.Add(cellToAdd);
        }
        return sorroundingCells;
    }
    public static void UpdateCellVariant(Cell cell){

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
    public static List<Chunk> GetSorroundingChunks(Vector3 position){
        int origin = GetChunk(position).index;

        List<Chunk> sorroundingChunks = new List<Chunk>();
        Chunk chunkToAdd = null;

        bool isValidChunk = false;

        for (int i = 0; i < 8; i++){
            isValidChunk = true;
            switch (i){
                case (0):
                    chunkToAdd = GetChunk(origin - (chunkLength - 1));
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
                case (1):
                    chunkToAdd = GetChunk(origin - chunkLength);
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
                case (2):
                    chunkToAdd = GetChunk(origin - (chunkLength + 1));
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
                case (3):
                    chunkToAdd = GetChunk(origin - 1);
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
                case (4):
                    chunkToAdd = GetChunk(origin + 1);
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
                case (5):
                    chunkToAdd = GetChunk(origin + (chunkLength - 1));
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
                case (6):
                    chunkToAdd = GetChunk(origin + chunkLength);
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
                case (7):
                    chunkToAdd = GetChunk(origin + (chunkLength + 1));
                    if (chunkToAdd == null) isValidChunk = false;
                    break;
            }
            if (isValidChunk) sorroundingChunks.Add(chunkToAdd);
        }
        return sorroundingChunks;
    }
    public class Cell{
        public bool isOccupied = false;

        public StructureObject structure = null;
        public GameObject gameobject = null;

        public Vector3 position = Vector3.zero;

        public int index = -1;
        public Cell(Vector3 _position, int _index){
            position = _position;
            index = _index;
        }
    }
    public class Chunk{
        public bool isLoaded = false;

        public List<Cell> cells = new List<Cell>();

        public Vector3 position = Vector3.zero;
        public Vector3 size = Vector3.zero;

        public int index = -1;

        public Chunk(List<Cell> _cells, Vector3 _position, Vector3 _size, int _index){
            cells = _cells;
            position = _position;
            size = _size;
            index = _index;
        }
    }
}



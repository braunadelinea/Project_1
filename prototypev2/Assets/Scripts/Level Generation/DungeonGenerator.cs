using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
class DungeonGenerator
{
    // Inner Classes
    public class RoomType
    {
        public readonly string name;
        public readonly bool north;
        public readonly bool east;
        public readonly bool south;
        public readonly bool west;

        internal RoomType(string name, bool north, bool east, bool south, bool west)
        {
            this.name = name;
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
        }

        public override string ToString()
        {
            return name;
        }
    }

    // Class Variables
    private RoomType[,] map;
    private List<RoomType> roomTypes = new List<RoomType>();
    private enum DoorPolicy { Required, Allowed, Blocked };
    private Tuple<int, int> firstRoomLocation;
    private Tuple<int, int> lastRoomLocation;

    private Random rand = new Random();
    private int endChance = 10;
    private int straightChance = 80;
    private int branchChance = 10;
    private int bendChance = 10;

    // Class Methods
    public DungeonGenerator(int size)
    {
        map = new RoomType[size, size];
        InitializeRoomTypes();
    }

    public void GenerateDungeon()
    {
        //TODO: clear dungeon

        Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();

        // generate first room
        int x = map.GetLength(0) / 2;
        int y = map.GetLength(1) / 2;
        firstRoomLocation = new Tuple<int, int>(x, y);
        AddRoom(x, y, new RoomType("NESW", true, true, true, true), queue);
        // while the queue is not empty, dequeue element, add room, push children to queue
        while (queue.Count != 0)
        {
            Tuple<int, int> location = queue.Dequeue();
            if (map[location.Item1, location.Item2] != null)
            {
                continue;
            }
            List<RoomType> legalRoomTypes = GetLegalRoomTypes(location.Item1, location.Item2);
            AddRoom(location.Item1, location.Item2, SelectRoom(legalRoomTypes), queue);
            lastRoomLocation = location;
        }
    }
    public void PrintDungeon()
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] != null)
                    {
                        switch (row)
                        {
                            case 0:
                                Console.Write("xx{0}xx", map[x, y].north ? " " : "x");
                                break;
                            case 1:
                                Console.Write("x   x");
                                break;
                            case 2:
                                Console.Write("{0}   {1}", map[x, y].west ? " " : "x", map[x, y].east ? " " : "x");
                                break;
                            case 3:
                                Console.Write("x   x");
                                break;
                            case 4:
                                Console.Write("xx{0}xx", map[x, y].south ? " " : "x");
                                break;
                        }
                    }
                    else
                    {
                        Console.Write("     ");
                    }
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine();
    }

    private RoomType SelectRoom(List<RoomType> legalRoomTypes)
    {
        List<RoomType> endRooms = new List<RoomType>();
        List<RoomType> straightRooms = new List<RoomType>();
        List<RoomType> branchRooms = new List<RoomType>();
        List<RoomType> bendRooms = new List<RoomType>();
        foreach (RoomType roomType in legalRoomTypes)
        {
            if (roomType.name == "Nesw" || roomType.name == "nEsw" || roomType.name == "neSw" || roomType.name == "nesW")
            {
                endRooms.Add(roomType);
            }
            else if (roomType.name == "NeSw" || roomType.name == "nEsW")
            {
                straightRooms.Add(roomType);
            }
            else if (roomType.name == "NESw" || roomType.name == "NEsW" || roomType.name == "NeSW" || roomType.name == "nESW" || roomType.name == "NESW")
            {
                branchRooms.Add(roomType);
            }
            else if (roomType.name == "NEsw" || roomType.name == "nESw" || roomType.name == "neSW" || roomType.name == "NesW")
            {
                bendRooms.Add(roomType);
            }
            else
            {
                Debug.Assert(false, "Unexpected room type");
            }
        }
        int totalChance = endChance + straightChance + branchChance + bendChance;
        int chance = rand.Next() % totalChance;
        if (chance < endChance)
        {
            if (endRooms.Count > 0)
            {
                return endRooms[rand.Next() % endRooms.Count];
            }
        }
        else if (chance < endChance + straightChance)
        {
            if (straightRooms.Count > 0)
            {
                return straightRooms[rand.Next() % straightRooms.Count];
            }
        }
        else if (chance < endChance + straightChance + branchChance)
        {
            if (branchRooms.Count > 0)
            {
                return branchRooms[rand.Next() % branchRooms.Count];
            }
        }
        else if (chance < endChance + straightChance + branchChance + bendChance)
        {
            if (bendRooms.Count > 0)
            {
                return bendRooms[rand.Next() % bendRooms.Count];
            }
        }
        else
        {
            Debug.Assert(false, "Logic failure, should have picked a room type");
        }
        return legalRoomTypes[rand.Next() % legalRoomTypes.Count];
    }

    private void AddRoom(int x, int y, RoomType roomType, Queue<Tuple<int, int>> queue)
    {
        Debug.Assert(x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1), "Coordinates passed in are out of bounds of the map");
        Debug.Assert(map[x, y] == null, "Attempted to add a room over an existing room");
        map[x, y] = roomType;
        if (roomType.north && map[x, y - 1] == null)
        {
            queue.Enqueue(new Tuple<int, int>(x, y - 1));
        }
        if (roomType.east && map[x + 1, y] == null)
        {
            queue.Enqueue(new Tuple<int, int>(x + 1, y));
        }
        if (roomType.south && map[x, y + 1] == null)
        {
            queue.Enqueue(new Tuple<int, int>(x, y + 1));
        }
        if (roomType.west && map[x - 1, y] == null)
        {
            queue.Enqueue(new Tuple<int, int>(x - 1, y));
        }
    }

    private List<RoomType> GetLegalRoomTypes(int x, int y)
    {
        Debug.Assert(x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1), "Coordinates passed in are out of bounds of the map");

        DoorPolicy northDoorPolicy = DoorPolicy.Blocked;
        if (y > 0)
        {
            RoomType otherRoomType = map[x, y - 1];
            if (otherRoomType == null)
            {
                northDoorPolicy = DoorPolicy.Allowed;
            }
            else if (otherRoomType.south)
            {
                northDoorPolicy = DoorPolicy.Required;
            }
        }

        DoorPolicy eastDoorPolicy = DoorPolicy.Blocked;
        if (x < map.GetLength(0) - 1)
        {
            RoomType otherRoomType = map[x + 1, y];
            if (otherRoomType == null)
            {
                eastDoorPolicy = DoorPolicy.Allowed;
            }
            else if (otherRoomType.west)
            {
                eastDoorPolicy = DoorPolicy.Required;
            }
        }

        DoorPolicy southDoorPolicy = DoorPolicy.Blocked;
        if (y < map.GetLength(1) - 1)
        {
            RoomType otherRoomType = map[x, y + 1];
            if (otherRoomType == null)
            {
                southDoorPolicy = DoorPolicy.Allowed;
            }
            else if (otherRoomType.north)
            {
                southDoorPolicy = DoorPolicy.Required;
            }
        }

        DoorPolicy westDoorPolicy = DoorPolicy.Blocked;
        if (x > 0)
        {
            RoomType otherRoomType = map[x - 1, y];
            if (otherRoomType == null)
            {
                westDoorPolicy = DoorPolicy.Allowed;
            }
            else if (otherRoomType.east)
            {
                westDoorPolicy = DoorPolicy.Required;
            }
        }

        return GetLegalRoomTypes(northDoorPolicy, eastDoorPolicy, southDoorPolicy, westDoorPolicy);
    }

    private List<RoomType> GetLegalRoomTypes(DoorPolicy northDoorPolicy, DoorPolicy eastDoorPolicy, DoorPolicy southDoorPolicy, DoorPolicy westDoorPolicy)
    {
        List<RoomType> legalRoomTypes = new List<RoomType>();
        foreach (RoomType roomType in roomTypes)
        {
            if (roomType.north && northDoorPolicy == DoorPolicy.Blocked)
            {
                continue;
            }
            if (!roomType.north && northDoorPolicy == DoorPolicy.Required)
            {
                continue;
            }
            if (roomType.east && eastDoorPolicy == DoorPolicy.Blocked)
            {
                continue;
            }
            if (!roomType.east && eastDoorPolicy == DoorPolicy.Required)
            {
                continue;
            }
            if (roomType.south && southDoorPolicy == DoorPolicy.Blocked)
            {
                continue;
            }
            if (!roomType.south && southDoorPolicy == DoorPolicy.Required)
            {
                continue;
            }
            if (roomType.west && westDoorPolicy == DoorPolicy.Blocked)
            {
                continue;
            }
            if (!roomType.west && westDoorPolicy == DoorPolicy.Required)
            {
                continue;
            }
            legalRoomTypes.Add(roomType);
        }
        return legalRoomTypes;
    }

    private void InitializeRoomTypes()
    {
        roomTypes.Add(new RoomType("Nesw", true, false, false, false));
        roomTypes.Add(new RoomType("nEsw", false, true, false, false));
        roomTypes.Add(new RoomType("neSw", false, false, true, false));
        roomTypes.Add(new RoomType("nesW", false, false, false, true));

        roomTypes.Add(new RoomType("NEsw", true, true, false, false));
        roomTypes.Add(new RoomType("NeSw", true, false, true, false));
        roomTypes.Add(new RoomType("NesW", true, false, false, true));
        roomTypes.Add(new RoomType("nESw", false, true, true, false));
        roomTypes.Add(new RoomType("nEsW", false, true, false, true));
        roomTypes.Add(new RoomType("neSW", false, false, true, true));

        roomTypes.Add(new RoomType("NESw", true, true, true, false));
        roomTypes.Add(new RoomType("NEsW", true, true, false, true));
        roomTypes.Add(new RoomType("NeSW", true, false, true, true));
        roomTypes.Add(new RoomType("nESW", false, true, true, true));

        roomTypes.Add(new RoomType("NESW", true, true, true, true));
    }

    public RoomType[,] getMap()
    {
        return map;
    }

    public Tuple<int, int> FirstRoomLocation
    {
        get
        {
            return firstRoomLocation;
        }
    }
    public Tuple<int, int> LastRoomLocation
    {
        get
        {
            return lastRoomLocation;
        }
    }

}

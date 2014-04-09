﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SettlersOfCatan;

namespace SettlersOfCatan
{
    public interface IEnums
    {
        public enum GAME_PIECE { NONE, SETTLEMENT, CITY };
        public enum PLAYER_COLOR { NONE, PLAYER1_COLOR, PLAYER2_COLOR, PLAYER3_COLOR, PLAYER4_COLOR };




    }

    public class IslandMap
    {

        
        public Intersection[,] map = new Intersection[6,11];

        public IslandMap()
        {
            initMap();
            setupConnections();
        }

        private void initMap()
        {
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 11; c++)
                {
                    map[r, c] = new Intersection(new Point(r, c));
                }
            }
        }

        private void setupConnections()
        {
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 11; c++)
                {
                    // Ignore the four corners of the grid that do not actually get used on the board
                    if ( ((r == 0 || r == 5) && (c < 2 || c > 8)) || ((r == 1 || r == 4) && (c < 1 || c > 9)) )
                    {
                        map[r, c] = null;
                    }
                    else
                    {
                        // Does not have a left connection (connection1)
                        if ( ((r == 0 || r == 5) && (c == 2)) || ((r == 1 || r == 4) && (c == 1)) || ((r == 2 || r == 3) && (c == 0)) )
                        {
                            
                            map[r, c].connections[0] = new Connection(null);
                            map[r, c].connections[2] = new Connection(map[r, c + 1]);
                            //map[r, c].connections.RemoveAt(0);
                        }
                        // Does not have a right connection (connection3)
                        else if( ((r == 0 || r == 5) && (c == 8)) || ((r == 1 || r == 4) && (c == 9)) || ((r == 2 || r == 3) && (c == 10)) ){
                            map[r, c].connections[0] = new Connection(map[r, c - 1]);
                            //map[r, c].connections.RemoveAt(2);
                            map[r, c].connections[2] = new Connection(null);
                        }
                        else{
                            // Left (connection1) and right (connection3) connections
                            map[r, c].connections[0] = new Connection(map[r, c - 1]);
                            map[r, c].connections[2] = new Connection(map[r, c + 1]);
                        }

                        int mapRow;
                        if ((r == 0 || r == 5) && c % 2 == 1)
                        {
                            map[r, c].connections[1] = new Connection(null);
                            //map[r, c].connections.RemoveAt(1);
                        }
                        else
                        {
                            // Determine if top or bottom connection (connection2)
                            if ((r + c) % 2 == 0) mapRow = r + 1; // Connect downward 
                            else mapRow = r - 1; // Connect upward

                            map[r, c].connections[1] = new Connection(map[mapRow, c]);
                        }
                    }
                }
            }


        }

        public Intersection getIntAtIndex(int x, int y)
        {
            Intersection i = map[x, y];
            return i;
        }



        public void buildSettlement(int x, int y)
        {
            map[x, y].build(IEnums.GAME_PIECE.SETTLEMENT);
        }

        

        
        


    }
}
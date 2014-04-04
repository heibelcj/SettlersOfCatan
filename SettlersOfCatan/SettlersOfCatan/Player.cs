﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SettlersOfCatan
{
    public class Player
    {
        private readonly int MAX_CITIES = 4;
        private readonly int MAX_SETTLEMENTS = 5;
        private readonly int MAX_ROADS = 15;

        private int points;
        private int citiesPlayed;
        private int settlementsPlayed;
        private int roadsPlayed;
        private Hand playerHand;
        private Player playerToTradeWith;
        private int[] toTrade;
        private int[] toReceive;
        private String name;
        private Color color;
        private bool hasWon;
        private World world;

        public Player()
        {
            this.points = 0;
            this.citiesPlayed = 0;
            this.settlementsPlayed = 0;
            this.roadsPlayed = 0;
            this.playerHand = new Hand();
            this.toTrade = new int[5] { 0, 0, 0, 0, 0 };
            this.toReceive = new int[5] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith = null;
            this.hasWon = false;
            this.world = new World();
        }

        public Player(String playerName, Color playerColor, World world)
        {
            this.name = playerName;
            this.color = playerColor;
            this.points = 0;
            this.citiesPlayed = 0;
            this.settlementsPlayed = 0;
            this.roadsPlayed = 0;
            this.playerHand = new Hand();
            this.toTrade = new int[5] { 0, 0, 0, 0, 0 };
            this.toReceive = new int[5] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith = null;
            this.hasWon = false;
            this.world = world;
        }

        public String getName()
        {
            return this.name;
        }

        public Color getColor()
        {
            return this.color;
        }

        public int getCitiesRemaining()
        {
            return MAX_CITIES - citiesPlayed;
        }

        public int getSettlementsRemaining()
        {
            return MAX_SETTLEMENTS - settlementsPlayed;
        }

        public int getRoadsRemaining()
        {
            return MAX_ROADS - roadsPlayed;
        }

        public bool incrementCities()
        {
            if (getCitiesRemaining() > 0)
            {
                citiesPlayed++;
                return true;
            }
            else
                return false;
        }

        public bool incrementSettlements()
        {
            if (getSettlementsRemaining() > 0)
            {
                settlementsPlayed++;
                return true;
            }
            else
                return false;
        }

        public bool incrementRoads()
        {
            if (getRoadsRemaining() > 0)
            {
                roadsPlayed++;
                return true;
            }
            else
                return false;
        }

        public void proposeTrade(Player player, int[] trade, int[] recieve)
        {
            this.toTrade = trade;
            this.toReceive = recieve;
            playerToTradeWith = player;
            player.toReceive = trade;
            player.toTrade = recieve;
        }

        private bool canAcceptTrade()
        {
            if (this.playerToTradeWith.playerHand.getOre() >= this.toReceive[0] && 
                this.playerToTradeWith.playerHand.getWool() >= this.toReceive[1] && 
                this.playerToTradeWith.playerHand.getLumber() >= this.toReceive[2] && 
                this.playerToTradeWith.playerHand.getGrain() >= this.toReceive[3] && 
                this.playerToTradeWith.playerHand.getBrick() >= this.toReceive[4] && 
                this.playerHand.getOre() >= this.toTrade[0] && 
                this.playerHand.getWool() >= this.toTrade[1] && 
                this.playerHand.getLumber() >= this.toTrade[2] && 
                this.playerHand.getGrain() >= this.toTrade[3] && 
                this.playerHand.getBrick() >= this.toTrade[4])
            {
                return true;
            }
            else
                return false;
        }


        public void acceptTrade()
        {
            if (this.canAcceptTrade())
            {
                this.playerHand.modifyOre(this.toReceive[0] - this.toTrade[0]);
                this.playerHand.modifyWool(this.toReceive[1] - this.toTrade[1]);
                this.playerHand.modifyLumber(this.toReceive[2] - this.toTrade[2]);
                this.playerHand.modifyGrain(this.toReceive[3] - this.toTrade[3]);
                this.playerHand.modifyBrick(this.toReceive[4] - this.toTrade[4]);
                this.playerToTradeWith.acceptTrade();
            }
            else
                throw new System.ArgumentException("Player's cards are such that trade cannot be performed");
        }

        public void declineTrade()
        {
            this.toTrade = new int[] { 0, 0, 0, 0, 0 };
            this.toReceive = new int[] { 0, 0, 0, 0, 0 };
            this.playerToTradeWith.declineTrade();
            this.playerToTradeWith = null;
        }

        public Hand getHand()
        {
            return playerHand;
        }

        public void incrementPoints(int amount)
        {
            this.points += amount;
            if (this.points >= 10)
                this.hasWon = true;
        }

        public bool hasWonGame()
        {
            return this.hasWon;
        }


        public void tradeWithBank(String tradeIn, String payOut)
        {
            if (tradeIn.ToLower().Equals("ore"))
            {
                if (getHand().getOre() >= 4)
                {
                    this.world.bank.modifyResource("ore",4);
                    this.world.bank.modifyResource(payOut, -1);
                }
            }
            else if (tradeIn.ToLower().Equals("wool"))
            {
                if (getHand().getWool() >= 4)
                {
                    this.world.bank.modifyResource("wool", 4);
                    this.world.bank.modifyResource(payOut, -1);
                }

            }
            else if (tradeIn.ToLower().Equals("lumber"))
            {
                if (getHand().getLumber() >= 4)
                {
                    this.world.bank.modifyResource("lumber", 4);
                    this.world.bank.modifyResource(payOut, -1);
                }

            }
            else if (tradeIn.ToLower().Equals("grain"))
            {
                if (getHand().getGrain() >= 4)
                {
                    this.world.bank.modifyResource("grain", 4);
                    this.world.bank.modifyResource(payOut, -1);
                }

            }
            else if (tradeIn.ToLower().Equals("brick"))
            {
                if (getHand().getBrick() >= 4)
                {
                    this.world.bank.modifyResource("brick", 4);
                    this.world.bank.modifyResource(payOut, -1);
                }
            }
            else if (tradeIn.ToLower().Equals("devcard"))
            {
                if (getHand().getOre() >= 1 && getHand().getGrain() >= 1 && getHand().getWool() >= 1)
                {
                    this.world.bank.modifyResource("ore", 1);
                    this.world.bank.modifyResource("wool", 1);
                    this.world.bank.modifyResource("grain", 1);
                    this.world.bank.modifyResource(payOut, -1);
                }
            }
        }

        // Need to know if port trades 2 or 3 for 1
        public void tradeAtPort(int portType, String resource)
        {
            //TODO reference bank inside of world class
            if (resource.ToLower().Equals("ore"))
            {
                if (getHand().getOre() >= 1)
                {
                    this.world.bank.modifyResource("ore", -1);
                    //this.world.bank.modifyResource(resourcePortTradesIn, -1);
                }
            }
            else if (resource.ToLower().Equals("wool"))
            {
                if (getHand().getWool() >= 1)
                {
                    this.world.bank.modifyResource("wool", -1);
                    //this.world.bank.modifyResource(resourcePortTradesIn, -1);
                }

            }
            else if (resource.ToLower().Equals("lumber"))
            {
                if (getHand().getLumber() >= 1)
                {
                    this.world.bank.modifyResource("lumber", -1);
                    //this.world.bank.modifyResource(resourcePortTradesIn, -1);
                }

            }
            else if (resource.ToLower().Equals("grain"))
            {
                if (getHand().getGrain() >= 1)
                {
                    this.world.bank.modifyResource("grain", -1);
                    //this.world.bank.modifyResource(resourcePortTradesIn, -1);
                }

            }
            else if (resource.ToLower().Equals("brick"))
            {
                if (getHand().getBrick() >= 1)
                {
                    this.world.bank.modifyResource("brick", 1);
                    //this.world.bank.modifyResource(resourcePortTradesIn, -1);
                }
            }
        }
    }
}

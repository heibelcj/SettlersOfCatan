﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlersOfCatan;
using System.Drawing;

namespace SettlersOfCatan
{
	public class Connection
	{
		public Intersection connectedTo;
		private Color roadColor;
		private bool built;
        private Point coords;

		public Connection(Intersection i)
		{
			connectedTo = i;
			built = false;
			roadColor = Color.White;
		}

		public Intersection getIntersection()
		{
			return connectedTo;
		}

		public void setRoadColor(Color c)
		{
			this.roadColor = c;
		}

		public Color getRoadColor()
		{
			return roadColor;
		}

		public void buildRoad(Color c, Point loc)
		{
			this.built = true;
			this.roadColor = c;
            this.coords = loc;
		}

		public bool isBuilt()
		{
			return built;
		}

        public Point getCoords()
        {
            return this.coords;
        }
	}
}
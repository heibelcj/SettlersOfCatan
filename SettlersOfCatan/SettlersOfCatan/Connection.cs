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

        public Connection(Intersection i)
        {
            connectedTo = i;
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

    }
}

using Mapsui.Geometries;
using Mapsui.Providers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tiles.Tools;

namespace MemoryProvider
{
    public class GridMemoryProvider : IProvider
    {
        private readonly double _maxVisible;
        private static int _counter;

        public GridMemoryProvider(double maxVisible)
        {
            this._maxVisible = maxVisible;
        }

        public string CRS { get; set; }

        public BoundingBox GetExtents()
        {
            return new BoundingBox(-180, - 90, 180, 90);
        }

        public IEnumerable<IFeature> GetFeaturesInView(BoundingBox box, double resolution)
        {
            if (!double.IsNaN(box.Top) && resolution < _maxVisible )
            {
                Debug.WriteLine($"GetFeaturesInView called ({_counter++}): " + box + ": " + resolution);
                var polygons = new List<Polygon>();
                var tiles = Tilebelt.GetTilesOnLevel(new [] { box.MinX, box.MinY, box.MaxX, box.MaxY}, 14);
                foreach(var t in tiles)
                {
                    var polygon = GetPolygon(t); 
                    polygons.Add(polygon);
                }

                var selected = polygons.Select(g => new Feature { Geometry = g }).ToList();
                return selected;

            }
            return null;
        }


        public Polygon GetPolygon(Tile tile)
        {
            var p = new Polygon();
            p.ExteriorRing.Vertices.Add(ToMapsui(tile.BoundsLL()));
            p.ExteriorRing.Vertices.Add(ToMapsui(tile.BoundsUL()));
            p.ExteriorRing.Vertices.Add(ToMapsui(tile.BoundsUR()));
            p.ExteriorRing.Vertices.Add(ToMapsui(tile.BoundsLR()));
            p.ExteriorRing.Vertices.Add(ToMapsui(tile.BoundsLL()));
            return p;
        }

        public static Point ToMapsui(Point2 point)
        {
            var p = new Point(point.X, point.Y);
            return p;
        }


    }
}

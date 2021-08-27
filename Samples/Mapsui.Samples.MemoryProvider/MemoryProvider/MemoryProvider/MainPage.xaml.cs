using Mapsui;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Styles;
using Mapsui.Utilities;
using System;
using Xamarin.Forms;
using Color = Mapsui.Styles.Color;

namespace MemoryProvider
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            var map = new Map
            {
                CRS = "EPSG:3857",
                Transformation = new MinimalTransformation()
            };

            var tileLayer = OpenStreetMap.CreateTileLayer();

            map.Layers.Add(tileLayer);
            map.Layers.Add(CreateGridLayer("Grid", new Color(150, 150, 30, 64), new Color(252, 76, 2, 128), 40));

            map.Widgets.Add(new Mapsui.Widgets.ScaleBar.ScaleBarWidget(map) { TextAlignment = Mapsui.Widgets.Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Left, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom });

            mapView.Map = map;
            var transformation = new MinimalTransformation();
            var p1 = transformation.Transform("EPSG:4326", "EPSG:3857", new Mapsui.Geometries.Point(5, 50));

            mapView.Navigator.CenterOn((Mapsui.Geometries.Point)p1);
            mapView.Navigator.ZoomTo(35);
        }


        public ILayer CreateGridLayer(string layerName, Color fillColor, Color outlineColor, double maxVisible)
        {
            return new Layer(layerName)
            {
                DataSource = new GridMemoryProvider(maxVisible) { CRS = "EPSG:4326" },
                Style = new VectorStyle
                {
                    Fill = new Mapsui.Styles.Brush(fillColor),
                    Outline = new Pen
                    {
                        Color = outlineColor,
                        Width = 4,
                        PenStyle = PenStyle.Solid,
                        PenStrokeCap = PenStrokeCap.Round
                    }
                },
                MaxVisible = maxVisible
            };
        }
    }
}

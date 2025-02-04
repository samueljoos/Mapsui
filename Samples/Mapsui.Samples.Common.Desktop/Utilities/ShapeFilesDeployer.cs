﻿using System;
using System.IO;
using System.Reflection;
using Mapsui.Samples.Common.Extensions;
using Mapsui.Samples.Common.Maps;

namespace Mapsui.Samples.Common.Desktop.Utilities
{
    public static class ShapeFilesDeployer
    {
        public static string ShapeFilesLocation { get; set; } =  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mapsui.Samples");

        public static void CopyEmbeddedResourceToFile(string shapefile)
        {
            shapefile = Path.GetFileNameWithoutExtension(shapefile);
            var assembly = typeof(ShapefileSample).GetTypeInfo().Assembly;
            assembly.CopyEmbeddedResourceToFile("Mapsui.Samples.Common.Desktop.GeoData.World.", ShapeFilesLocation, shapefile + ".dbf");
            assembly.CopyEmbeddedResourceToFile("Mapsui.Samples.Common.Desktop.GeoData.World.", ShapeFilesLocation, shapefile + ".prj");
            assembly.CopyEmbeddedResourceToFile("Mapsui.Samples.Common.Desktop.GeoData.World.", ShapeFilesLocation, shapefile + ".shp");
            assembly.CopyEmbeddedResourceToFile("Mapsui.Samples.Common.Desktop.GeoData.World.", ShapeFilesLocation, shapefile + ".shx");
            assembly.CopyEmbeddedResourceToFile("Mapsui.Samples.Common.Desktop.GeoData.World.", ShapeFilesLocation, shapefile + ".shp.sidx");
        }
    }
}

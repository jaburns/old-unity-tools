
using UnityEditor;

public class AssetImportDefaults : AssetPostprocessor
{
    /// <summary>
    /// Ensures that FBX files get imported with a default scale of 1 instead of 0.01.
    /// </summary>
    void OnPreprocessModel()
    {
        var modelImporter = (ModelImporter) assetImporter;
        modelImporter.animationType = ModelImporterAnimationType.Legacy;
        modelImporter.globalScale = 1;  
    }   
}
using UnityEngine;
using TMPro;

/// <summary>
/// Editor utility to generate placeholder 3D models for testing
/// Attach this to an empty GameObject and use the context menu to generate models
/// </summary>
public class PlaceholderModelGenerator : MonoBehaviour
{
    [Header("Generated Prefabs Output")]
    public GameObject solarSystemPlaceholder;
    public GameObject plantCellPlaceholder;
    public GameObject digestiveSystemPlaceholder;
    public GameObject waterCyclePlaceholder;
    public GameObject atomPlaceholder;
    
    [ContextMenu("Generate All Placeholder Models")]
    public void GenerateAllPlaceholders()
    {
        solarSystemPlaceholder = CreateSolarSystem();
        plantCellPlaceholder = CreatePlantCell();
        digestiveSystemPlaceholder = CreateDigestiveSystem();
        waterCyclePlaceholder = CreateWaterCycle();
        atomPlaceholder = CreateAtom();
        
        Debug.Log("All placeholder models generated! Drag them to the Manager script.");
    }
    
    GameObject CreateSolarSystem()
    {
        GameObject parent = new GameObject("SolarSystem_Placeholder");
        parent.AddComponent<ARModelInteraction>();
        
        // Sun
        GameObject sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.name = "Sun";
        sun.transform.SetParent(parent.transform);
        sun.transform.localPosition = Vector3.zero;
        sun.transform.localScale = Vector3.one * 0.15f;
        SetColor(sun, Color.yellow);
        
        // Planets
        CreateOrbitingPlanet(parent, "Mercury", 0.02f, 0.2f, new Color(0.5f, 0.5f, 0.5f));
        CreateOrbitingPlanet(parent, "Venus", 0.03f, 0.3f, new Color(0.9f, 0.7f, 0.5f));
        CreateOrbitingPlanet(parent, "Earth", 0.035f, 0.4f, Color.blue);
        CreateOrbitingPlanet(parent, "Mars", 0.025f, 0.5f, Color.red);
        
        return parent;
    }
    
    void CreateOrbitingPlanet(GameObject parent, string name, float size, float distance, Color color)
    {
        GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planet.name = name;
        planet.transform.SetParent(parent.transform);
        planet.transform.localPosition = new Vector3(distance, 0, 0);
        planet.transform.localScale = Vector3.one * size;
        SetColor(planet, color);
    }
    
    GameObject CreatePlantCell()
    {
        GameObject parent = new GameObject("PlantCell_Placeholder");
        parent.AddComponent<ARModelInteraction>();
        
        // Cell wall (outer box)
        GameObject cellWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cellWall.name = "CellWall";
        cellWall.transform.SetParent(parent.transform);
        cellWall.transform.localScale = new Vector3(0.3f, 0.2f, 0.25f);
        SetColor(cellWall, new Color(0.6f, 0.8f, 0.6f, 0.5f));
        
        // Nucleus
        GameObject nucleus = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        nucleus.name = "Nucleus";
        nucleus.transform.SetParent(parent.transform);
        nucleus.transform.localPosition = new Vector3(0.05f, 0, 0);
        nucleus.transform.localScale = Vector3.one * 0.08f;
        SetColor(nucleus, new Color(0.4f, 0.2f, 0.6f));
        
        // Chloroplasts
        for (int i = 0; i < 4; i++)
        {
            GameObject chloroplast = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            chloroplast.name = $"Chloroplast_{i}";
            chloroplast.transform.SetParent(parent.transform);
            chloroplast.transform.localPosition = new Vector3(
                Random.Range(-0.1f, 0.1f),
                Random.Range(-0.05f, 0.05f),
                Random.Range(-0.08f, 0.08f)
            );
            chloroplast.transform.localScale = new Vector3(0.02f, 0.04f, 0.02f);
            SetColor(chloroplast, Color.green);
        }
        
        // Vacuole
        GameObject vacuole = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        vacuole.name = "Vacuole";
        vacuole.transform.SetParent(parent.transform);
        vacuole.transform.localPosition = new Vector3(-0.05f, 0, 0);
        vacuole.transform.localScale = new Vector3(0.12f, 0.1f, 0.1f);
        SetColor(vacuole, new Color(0.7f, 0.9f, 1f, 0.7f));
        
        return parent;
    }
    
    GameObject CreateDigestiveSystem()
    {
        GameObject parent = new GameObject("DigestiveSystem_Placeholder");
        parent.AddComponent<ARModelInteraction>();
        
        // Esophagus
        GameObject esophagus = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        esophagus.name = "Esophagus";
        esophagus.transform.SetParent(parent.transform);
        esophagus.transform.localPosition = new Vector3(0, 0.15f, 0);
        esophagus.transform.localScale = new Vector3(0.03f, 0.08f, 0.03f);
        SetColor(esophagus, new Color(0.9f, 0.6f, 0.6f));
        
        // Stomach
        GameObject stomach = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        stomach.name = "Stomach";
        stomach.transform.SetParent(parent.transform);
        stomach.transform.localPosition = new Vector3(0.03f, 0.05f, 0);
        stomach.transform.localScale = new Vector3(0.1f, 0.08f, 0.08f);
        SetColor(stomach, new Color(0.9f, 0.5f, 0.5f));
        
        // Small Intestine (coiled cylinder representation)
        GameObject smallIntestine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        smallIntestine.name = "SmallIntestine";
        smallIntestine.transform.SetParent(parent.transform);
        smallIntestine.transform.localPosition = new Vector3(0, -0.05f, 0);
        smallIntestine.transform.localScale = new Vector3(0.12f, 0.05f, 0.12f);
        SetColor(smallIntestine, new Color(1f, 0.7f, 0.7f));
        
        // Large Intestine
        GameObject largeIntestine = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        largeIntestine.name = "LargeIntestine";
        largeIntestine.transform.SetParent(parent.transform);
        largeIntestine.transform.localPosition = new Vector3(0, -0.15f, 0);
        largeIntestine.transform.localScale = new Vector3(0.15f, 0.03f, 0.1f);
        SetColor(largeIntestine, new Color(0.8f, 0.5f, 0.4f));
        
        return parent;
    }
    
    GameObject CreateWaterCycle()
    {
        GameObject parent = new GameObject("WaterCycle_Placeholder");
        parent.AddComponent<ARModelInteraction>();
        
        // Ground/Ocean
        GameObject ocean = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ocean.name = "Ocean";
        ocean.transform.SetParent(parent.transform);
        ocean.transform.localPosition = new Vector3(0, -0.1f, 0);
        ocean.transform.localScale = new Vector3(0.4f, 0.02f, 0.3f);
        SetColor(ocean, new Color(0.2f, 0.4f, 0.8f));
        
        // Mountain
        GameObject mountain = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        mountain.name = "Mountain";
        mountain.transform.SetParent(parent.transform);
        mountain.transform.localPosition = new Vector3(0.12f, 0, 0);
        mountain.transform.localScale = new Vector3(0.1f, 0.15f, 0.1f);
        SetColor(mountain, new Color(0.4f, 0.3f, 0.2f));
        
        // Clouds
        for (int i = 0; i < 3; i++)
        {
            GameObject cloud = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            cloud.name = $"Cloud_{i}";
            cloud.transform.SetParent(parent.transform);
            cloud.transform.localPosition = new Vector3(-0.1f + i * 0.1f, 0.15f, 0);
            cloud.transform.localScale = new Vector3(0.08f, 0.04f, 0.06f);
            SetColor(cloud, Color.white);
        }
        
        // Rain drops
        for (int i = 0; i < 5; i++)
        {
            GameObject rain = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            rain.name = $"Rain_{i}";
            rain.transform.SetParent(parent.transform);
            rain.transform.localPosition = new Vector3(-0.05f + i * 0.03f, 0.08f - i * 0.02f, 0);
            rain.transform.localScale = new Vector3(0.01f, 0.02f, 0.01f);
            SetColor(rain, new Color(0.5f, 0.7f, 1f));
        }
        
        // Sun
        GameObject sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.name = "Sun";
        sun.transform.SetParent(parent.transform);
        sun.transform.localPosition = new Vector3(-0.15f, 0.12f, 0.1f);
        sun.transform.localScale = Vector3.one * 0.06f;
        SetColor(sun, Color.yellow);
        
        return parent;
    }
    
    GameObject CreateAtom()
    {
        GameObject parent = new GameObject("Atom_Placeholder");
        parent.AddComponent<ARModelInteraction>();
        
        // Nucleus
        GameObject nucleus = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        nucleus.name = "Nucleus";
        nucleus.transform.SetParent(parent.transform);
        nucleus.transform.localScale = Vector3.one * 0.06f;
        SetColor(nucleus, Color.red);
        
        // Protons and Neutrons in nucleus
        for (int i = 0; i < 4; i++)
        {
            GameObject proton = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            proton.name = $"Proton_{i}";
            proton.transform.SetParent(parent.transform);
            proton.transform.localPosition = new Vector3(
                Mathf.Cos(i * 90 * Mathf.Deg2Rad) * 0.02f,
                Mathf.Sin(i * 90 * Mathf.Deg2Rad) * 0.02f,
                0
            );
            proton.transform.localScale = Vector3.one * 0.025f;
            SetColor(proton, i % 2 == 0 ? Color.red : Color.blue);
        }
        
        // Electron orbits
        CreateElectronOrbit(parent, 0.12f, 0, Color.cyan, "Electron_1");
        CreateElectronOrbit(parent, 0.12f, 60, Color.cyan, "Electron_2");
        CreateElectronOrbit(parent, 0.18f, 30, Color.cyan, "Electron_3");
        
        return parent;
    }
    
    void CreateElectronOrbit(GameObject parent, float radius, float angleOffset, Color color, string name)
    {
        // Electron
        GameObject electron = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        electron.name = name;
        electron.transform.SetParent(parent.transform);
        
        float angle = angleOffset * Mathf.Deg2Rad;
        electron.transform.localPosition = new Vector3(
            Mathf.Cos(angle) * radius,
            Mathf.Sin(angle) * radius * 0.5f,
            Mathf.Sin(angle) * radius * 0.3f
        );
        electron.transform.localScale = Vector3.one * 0.02f;
        SetColor(electron, color);
    }
    
    void SetColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Create a new material instance
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = color;
            
            // Handle transparency
            if (color.a < 1f)
            {
                mat.SetFloat("_Surface", 1); // Transparent
                mat.SetFloat("_Blend", 0);
                mat.SetOverrideTag("RenderType", "Transparent");
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
            
            renderer.material = mat;
        }
    }
}

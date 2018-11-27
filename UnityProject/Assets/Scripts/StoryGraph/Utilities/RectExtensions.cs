using UnityEngine;
 
// Helper Rect extension methods
public static class RectExtensions
{
    public static Vector2 TopLeft(this Rect rect)
    {
        return new Vector2(rect.xMin, rect.yMin);
    }
	public static Vector2 BottomRight(this Rect rect)
    {
        return new Vector2(rect.xMax, rect.yMax);
    }
    public static Rect ScaleSizeBy(this Rect rect, float scale)
    {
        return ScaleSizeBy(rect, scale, rect.center);
    }
    public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale;
        result.xMax *= scale;
        result.yMin *= scale;
        result.yMax *= scale;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }
    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
    {
        return ScaleSizeBy(rect, scale, rect.center);
    }
    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale.x;
        result.xMax *= scale.x;
        result.yMin *= scale.y;
        result.yMax *= scale.y;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }


    public static Rect Scale (Rect rect, Vector2 pivot, Vector2 scale) 
    {
        rect.position = Vector2.Scale (rect.position - pivot, scale) + pivot;
        rect.size = Vector2.Scale (rect.size, scale);
        return rect;
    }
}
 
// public class EditorZoomArea
// {
//     private const float kEditorWindowTabHeight = 21.0f;
//     private static Matrix4x4 _prevGuiMatrix;
 
//     public static Rect Begin(float zoomScale, Rect screenCoordsArea)
//     {
//         GUI.EndGroup();        // End the group Unity begins automatically for an EditorWindow to clip out the window tab. This allows us to draw outside of the size of the EditorWindow.
 
//         Rect clippedArea = RectExtensions.ScaleSizeBy(screenCoordsArea, 1.0f / zoomScale, RectExtensions.TopLeft(screenCoordsArea));
//         clippedArea.y += kEditorWindowTabHeight;
//         GUI.BeginGroup(clippedArea);
 
//         _prevGuiMatrix = GUI.matrix;
//         Matrix4x4 translation = Matrix4x4.TRS(RectExtensions.TopLeft(clippedArea), Quaternion.identity, Vector3.one);
//         Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));

//         GUI.matrix = translation * scale * translation.inverse * GUI.matrix;

 
//         return clippedArea;
//     }
 
//     public static void End()
//     {
//         GUI.matrix = _prevGuiMatrix;
//         GUI.EndGroup();
//         GUI.BeginGroup(new Rect(0.0f, kEditorWindowTabHeight, Screen.width, Screen.height));
//     }
// }
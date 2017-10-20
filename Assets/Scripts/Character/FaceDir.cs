using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FACE_DIR {
    LEFT = -1,
    RIGHT = 1
}

public static class FaceDirExtension {
    public static FACE_DIR Reverse (this FACE_DIR dir) {
        return (FACE_DIR)(-1 * (int)dir);
    }
}

public static class VectorExtension {

    public static FACE_DIR GenerateFaceDir (this Vector2 vec) {
        return (FACE_DIR)System.Math.Sign(vec.x);
    }

    public static FACE_DIR GenerateFaceDir (this Vector3 vec) {
        return (FACE_DIR)System.Math.Sign(vec.x);
    }
}

public static class TransformExtension {

    public static FACE_DIR GetFaceDir (this Transform transform) {
        return transform.localScale.GenerateFaceDir();
    }

    public static void SetFaceDir (this Transform transform, FACE_DIR dir) {
        Vector2 scale = (Vector2)transform.localScale;
        transform.localScale
            = new Vector2((float)dir * System.Math.Abs(scale.x), scale.y);
    }
}

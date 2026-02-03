using UnityEngine;

namespace Raumkapsel {

    public static class Ease {

        public static float CubicInOut(float p) {
            return p < 0.5f ? 4 * p * p * p : 1 - Mathf.Pow(-2 * p + 2, 3) / 2;
        }

        public static float BackIn(float p) {
            const float c1 = 4.70158f;
            const float c3 = c1 + 1;
            return c3 * p * p * p - c1 * p * p;
        }
    }
}
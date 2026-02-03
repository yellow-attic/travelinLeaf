
namespace Raumkapsel {

    public static class Ease {
        public static float BackIn(float p) {
            const float c1 = 2.70158f;
            const float c3 = c1 + 1;
            return c3 * p * p * p - c1 * p * p;
        }
    }
}
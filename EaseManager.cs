using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EasingFunction {
    none,
    easeInSine,
    easeInCubic,
    easeInQuint,
    easeInCirc,
    easeInElastic,
    easeInQuad,
    easeInQuart,
    easeInExpo,
    easeInBack,
    easeInBounce,

    easeOutSine,
    easeOutCubic,
    easeOutQuint,
    easeOutCirc,
    easeOutElastic,
    easeOutQuad,
    easeOutQuart,
    easeOutExpo,
    easeOutBack,
    easeOutBounce,

    easeInOutSine,
    easeInOutCubic,
    easeInOutQuint,
    easeInOutCirc,
    easeInOutElastic,
    easeInOutQuad,
    easeInOutQuart,
    easeInOutExpo,
    easeInOutBack,
    easeInOutBounce
}
public class EaseManager
{
    private EasingFunction m_function;
    private Func<float, float> easeFunction;

    public EaseManager(EasingFunction function)
    {
        m_function = function;
        easeFunction = GetEaseFunction(m_function);
    }

    public float Ease(float x)
    {
        Mathf.Clamp(x, 0, 1);
        return easeFunction != null ? easeFunction(x) : x;
    }

    private Func<float, float> GetEaseFunction(EasingFunction fun)
    {
        switch (fun)
        {
            case EasingFunction.easeInSine: return EaseMethods.EaseInSine;
            case EasingFunction.easeInCubic: return EaseMethods.EaseInCubic;
            case EasingFunction.easeInQuint: return EaseMethods.EaseInQuint;
            case EasingFunction.easeInCirc: return EaseMethods.EaseInCirc;
            case EasingFunction.easeInElastic: return EaseMethods.EaseInElastic;
            case EasingFunction.easeInQuad: return EaseMethods.EaseInQuad;
            case EasingFunction.easeInQuart: return EaseMethods.EaseInQuart;
            case EasingFunction.easeInExpo: return EaseMethods.EaseInExpo;
            case EasingFunction.easeInBack: return EaseMethods.EaseInBack;
            case EasingFunction.easeInBounce: return EaseMethods.EaseInBounce;

            case EasingFunction.easeOutSine: return EaseMethods.EaseOutSine;
            case EasingFunction.easeOutCubic: return EaseMethods.EaseOutCubic;
            case EasingFunction.easeOutQuint: return EaseMethods.EaseOutQuint;
            case EasingFunction.easeOutCirc: return EaseMethods.EaseOutCirc;
            case EasingFunction.easeOutElastic: return EaseMethods.EaseOutElastic;
            case EasingFunction.easeOutQuad: return EaseMethods.EaseOutQuad;
            case EasingFunction.easeOutQuart: return EaseMethods.EaseOutQuart;
            case EasingFunction.easeOutExpo: return EaseMethods.EaseOutExpo;
            case EasingFunction.easeOutBack: return EaseMethods.EaseOutBack;
            case EasingFunction.easeOutBounce: return EaseMethods.EaseOutBounce;

            case EasingFunction.easeInOutSine: return EaseMethods.EaseInOutSine;
            case EasingFunction.easeInOutCubic: return EaseMethods.EaseInOutCubic;
            case EasingFunction.easeInOutQuint: return EaseMethods.EaseInOutQuint;
            case EasingFunction.easeInOutCirc: return EaseMethods.EaseInOutCirc;
            case EasingFunction.easeInOutElastic: return EaseMethods.EaseInOutElastic;
            case EasingFunction.easeInOutQuad: return EaseMethods.EaseInOutQuad;
            case EasingFunction.easeInOutQuart: return EaseMethods.EaseInOutQuart;
            case EasingFunction.easeInOutExpo: return EaseMethods.EaseInOutExpo;
            case EasingFunction.easeInOutBack: return EaseMethods.EaseInOutBack;
            case EasingFunction.easeInOutBounce: return EaseMethods.EaseInOutBounce;
        }

        return null;
    }

    private static class EaseMethods 
    {
        // see https://easings.net/fr#
        private const float _PI = Mathf.PI;
        private const float _C1 = 1.70158f;
        private const float _C2 = _C1 * 1.525f;
        private const float _C3 = _C1 + 1;
        private const float _C4 = (2 * _PI) / 3f;
        private const float _C5 = (2 * _PI) / 4.5f;
        private const float _N1 = 7.5625f;
        private const float _D1 = 2.75f;

        #region EaseIn
        public static float EaseInSine(float x)
        {
            return 1 - (Mathf.Cos(_PI * x) / 2);
        }

        public static float EaseInCubic(float x)
        {
            return x * x * x;
        }

        public static float EaseInQuint(float x)
        {
            return x * x * x * x * x;
        }

        public static float EaseInCirc(float x)
        {
            return 1 - Mathf.Sqrt(1 - x * x);
        }

        public static float EaseInElastic(float x)
        {
            return x == 0
                ? 0
                : x == 1
                ? 1
                : - Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * _C4);
        }

        public static float EaseInQuad(float x)
        {
            return x * x;
        }

        public static float EaseInQuart(float x)
        {
            return x * x * x * x;
        }

        public static float EaseInExpo(float x)
        {
            return x == 0
            ? 0
            : Mathf.Pow(2, 10 * x - 10);
        }

        public static float EaseInBack(float x)
        {
            return _C3 * x * x * x - _C1 * x * x;
        }

        public static float EaseInBounce(float x)
        {
            return 1 - EaseOutBounce(1 - x);
        }
        #endregion

        #region EaseOut
        public static float EaseOutSine(float x)
        {
            return Mathf.Sin((x * _PI) / 2);
        }

        public static float EaseOutCubic(float x)
        {
            return 1 - Mathf.Pow(1 - x, 3);
        }

        public static float EaseOutQuint(float x)
        {
            return 1 - Mathf.Pow(1 - x, 5);
        }

        public static float EaseOutCirc(float x)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
        }

        public static float EaseOutElastic(float x)
        {
            return x == 0
                ? 0
                : x == 1
                ? 1
                : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - .75f) * _C4) + 1;
        }

        public static float EaseOutQuad(float x)
        {
            return 1 - (1 - x) * (1 - x);
        }

        public static float EaseOutQuart(float x)
        {
            return 1 - Mathf.Pow(1 - x, 4);
        }

        public static float EaseOutExpo(float x)
        {
            return x == 1
            ? 1
            : 1 - Mathf.Pow(2, -10 * x);
        }

        public static float EaseOutBack(float x)
        {
            return 1 + _C3 * Mathf.Pow(x - 1, 3) + _C1 * Mathf.Pow(x - 1, 2);
        }

        public static float EaseOutBounce(float x)
        {
            if (x < 1 / _D1)
                return _N1 * x * x;
            else if (x < 2 / _D1)
                return _N1 * (x -= 1.5f / _D1) * x + 0.75f;
            else if (x < 2.5f / _D1)
                return _N1 * (x -= 2.25f / _D1) * x + 0.9375f;
            else
                return _N1 * (x -= 2.625f / _D1) * x + 0.984375f;
        }
        #endregion

        #region EaseInOut
        public static float EaseInOutSine(float x)
        {
            return -(Mathf.Cos(_PI * x) - 1) / 2;
        }

        public static float EaseInOutCubic(float x)
        {
            return x < 0.5f 
                ? 4 * x * x * x
                : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }

        public static float EaseInOutQuint(float x)
        {
            return x < 0.5f
                ? 16 * x * x * x * x * x
                : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
        }

        public static float EaseInOutCirc(float x)
        {
            return x < 0.5f
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
        }

        public static float EaseInOutElastic(float x)
        {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5f
                        ? - (Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * _C5)) / 2
                        :  (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * _C5)) / 2 + 1;
        }

        public static float EaseInOutQuad(float x)
        {
            return x < 0.5f
                ? 2 * x * x
                : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
        }

        public static float EaseInOutQuart(float x)
        {
            return x < 0.5f
                ? 8 * x * x * x * x
                : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
        }

        public static float EaseInOutExpo(float x)
        {
            return x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5f
                        ? Mathf.Pow(2, 20 * x - 10) / 2
                        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
        }

        public static float EaseInOutBack(float x)
        {
            return x < 0.5f
                ? (Mathf.Pow(2 * x, 2) * ((_C2 + 1) * 2 * x - _C2)) / 2
                : (Mathf.Pow(2 * x - 2, 2) * ((_C2 + 1) * (x * 2 - 2) + _C2) + 2) / 2;
        }

        public static float EaseInOutBounce(float x)
        {
            return x < 0.5f
                ? (1 - EaseOutBounce(1 - 2 * x)) / 2
                : (1 + EaseOutBounce(2 * x - 1)) / 2;
        }
        #endregion
    }
}
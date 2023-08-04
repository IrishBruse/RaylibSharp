namespace RaylibSharp;

public static unsafe partial class Raylib
{
    // Linear Easing functions
    /// <summary> Ease: Linear </summary>
    public static float EaseLinearNone(float t, float b, float c, float d)
    {
        return (c * t / d) + b;
    }

    /// <summary> Ease: Linear In </summary>
    public static float EaseLinearIn(float t, float b, float c, float d)
    {
        return (c * t / d) + b;
    }

    /// <summary> Ease: Linear Out </summary>
    public static float EaseLinearOut(float t, float b, float c, float d)
    {
        return (c * t / d) + b;
    }

    /// <summary> Ease: Linear In Out </summary>
    public static float EaseLinearInOut(float t, float b, float c, float d)
    {
        return (c * t / d) + b;
    }

    // Sine Easing functions
    /// <summary> Ease: Sine In </summary>
    public static float EaseSineIn(float t, float b, float c, float d)
    {
        return (-c * MathF.Cos(t / d * (MathF.PI / 2.0f))) + c + b;
    }

    /// <summary> Ease: Sine Out </summary>
    public static float EaseSineOut(float t, float b, float c, float d)
    {
        return (c * MathF.Sin(t / d * (MathF.PI / 2.0f))) + b;
    }

    /// <summary> Ease: Sine Out </summary>
    public static float EaseSineInOut(float t, float b, float c, float d)
    {
        return (-c / 2.0f * (MathF.Cos(MathF.PI * t / d) - 1.0f)) + b;
    }

    // Circular Easing functions
    /// <summary> Ease: Sine Out </summary>
    public static float EaseCircIn(float t, float b, float c, float d)
    {
        t /= d; return (-c * (MathF.Sqrt(1.0f - (t * t)) - 1.0f)) + b;
    }

    /// <summary> Ease: Circular Out </summary>
    public static float EaseCircOut(float t, float b, float c, float d)
    {
        t = (t / d) - 1.0f; return (c * MathF.Sqrt(1.0f - (t * t))) + b;
    }

    /// <summary> Ease: Circular In Out </summary>
    public static float EaseCircInOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2.0f) < 1.0f)
        {
            return (-c / 2.0f * (MathF.Sqrt(1.0f - (t * t)) - 1.0f)) + b;
        }

        t -= 2.0f; return (c / 2.0f * (MathF.Sqrt(1.0f - (t * t)) + 1.0f)) + b;
    }

    // Cubic Easing functions
    /// <summary> Ease: Cubic In </summary>
    public static float EaseCubicIn(float t, float b, float c, float d)
    {
        t /= d; return (c * t * t * t) + b;
    }

    /// <summary> Ease: Cubic Out </summary>
    public static float EaseCubicOut(float t, float b, float c, float d)
    {
        t = (t / d) - 1.0f; return (c * ((t * t * t) + 1.0f)) + b;
    }

    /// <summary> Ease: Cubic In Out </summary>
    public static float EaseCubicInOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2.0f) < 1.0f)
        {
            return (c / 2.0f * t * t * t) + b;
        }

        t -= 2.0f; return (c / 2.0f * ((t * t * t) + 2.0f)) + b;
    }

    // Quadratic Easing functions
    /// <summary> Ease: Quadratic In </summary>
    public static float EaseQuadIn(float t, float b, float c, float d)
    {
        t /= d; return (c * t * t) + b;
    }

    /// <summary> Ease: Quadratic Out </summary>
    public static float EaseQuadOut(float t, float b, float c, float d)
    {
        t /= d; return (-c * t * (t - 2.0f)) + b;
    }

    /// <summary> Ease: Quadratic In Out </summary>
    public static float EaseQuadInOut(float t, float b, float c, float d)
    {
        if ((t /= d / 2) < 1)
        {
            return (c / 2 * (t * t)) + b;
        }

        return (-c / 2.0f * (((t - 1.0f) * (t - 3.0f)) - 1.0f)) + b;
    }

    // Exponential Easing functions
    /// <summary> Ease: Exponential In </summary>
    public static float EaseExpoIn(float t, float b, float c, float d)
    {
        return (t == 0.0f) ? b : ((c * MathF.Pow(2.0f, 10.0f * ((t / d) - 1.0f))) + b);
    }

    /// <summary> Ease: Exponential Out </summary>
    public static float EaseExpoOut(float t, float b, float c, float d)
    {
        return (t == d) ? (b + c) : ((c * (-MathF.Pow(2.0f, -10.0f * t / d) + 1.0f)) + b);
    }

    /// <summary> Ease: Exponential In Out </summary>
    public static float EaseExpoInOut(float t, float b, float c, float d)
    {
        if (t == 0.0f)
        {
            return b;
        }

        if (t == d)
        {
            return b + c;
        }

        if ((t /= d / 2.0f) < 1.0f)
        {
            return (c / 2.0f * MathF.Pow(2.0f, 10.0f * (t - 1.0f))) + b;
        }

        return (c / 2.0f * (-MathF.Pow(2.0f, -10.0f * (t - 1.0f)) + 2.0f)) + b;
    }

    // Back Easing functions
    /// <summary> Ease: Back In </summary>
    public static float EaseBackIn(float t, float b, float c, float d)
    {
        float s = 1.70158f;
        float postFix = t /= d;
        return (c * postFix * t * (((s + 1.0f) * t) - s)) + b;
    }

    /// <summary> Ease: Back Out </summary>
    public static float EaseBackOut(float t, float b, float c, float d)
    {
        float s = 1.70158f;
        t = (t / d) - 1.0f;
        return (c * ((t * t * (((s + 1.0f) * t) + s)) + 1.0f)) + b;
    }

    /// <summary> Ease: Back In Out </summary>
    public static float EaseBackInOut(float t, float b, float c, float d)
    {
        float s = 1.70158f;
        if ((t /= d / 2.0f) < 1.0f)
        {
            s *= 1.525f;
            return (c / 2.0f * (t * t * (((s + 1.0f) * t) - s))) + b;
        }

        float postFix = t -= 2.0f;
        s *= 1.525f;
        return (c / 2.0f * ((postFix * t * (((s + 1.0f) * t) + s)) + 2.0f)) + b;
    }

    // Bounce Easing functions
    /// <summary> Ease: Bounce Out </summary>
    public static float EaseBounceOut(float t, float b, float c, float d)
    {
        if ((t /= d) < (1.0f / 2.75f))
        {
            return (c * (7.5625f * t * t)) + b;
        }
        else if (t < (2.0f / 2.75f))
        {
            float postFix = t -= 1.5f / 2.75f;
            return (c * ((7.5625f * postFix * t) + 0.75f)) + b;
        }
        else if (t < (2.5 / 2.75))
        {
            float postFix = t -= 2.25f / 2.75f;
            return (c * ((7.5625f * postFix * t) + 0.9375f)) + b;
        }
        else
        {
            float postFix = t -= 2.625f / 2.75f;
            return (c * ((7.5625f * postFix * t) + 0.984375f)) + b;
        }
    }

    /// <summary> Ease: Bounce In </summary>
    public static float EaseBounceIn(float t, float b, float c, float d)
    {
        return c - EaseBounceOut(d - t, 0.0f, c, d) + b;
    }

    /// <summary> Ease: Bounce In Out </summary>
    public static float EaseBounceInOut(float t, float b, float c, float d)
    {
        if (t < d / 2.0f)
        {
            return (EaseBounceIn(t * 2.0f, 0.0f, c, d) * 0.5f) + b;
        }
        else
        {
            return (EaseBounceOut((t * 2.0f) - d, 0.0f, c, d) * 0.5f) + (c * 0.5f) + b;
        }
    }

    // Elastic Easing functions
    /// <summary> Ease: Elastic In </summary>
    public static float EaseElasticIn(float t, float b, float c, float d)
    {
        if (t == 0.0f)
        {
            return b;
        }

        if ((t /= d) == 1.0f)
        {
            return b + c;
        }

        float p = d * 0.3f;
        float a = c;
        float s = p / 4.0f;
        float postFix = a * MathF.Pow(2.0f, 10.0f * (t -= 1.0f));

        return -(postFix * MathF.Sin(((t * d) - s) * (2.0f * MathF.PI) / p)) + b;
    }

    /// <summary> Ease: Elastic Out </summary>
    public static float EaseElasticOut(float t, float b, float c, float d)
    {
        if (t == 0.0f)
        {
            return b;
        }

        if ((t /= d) == 1.0f)
        {
            return b + c;
        }

        float p = d * 0.3f;
        float a = c;
        float s = p / 4.0f;

        return (a * MathF.Pow(2.0f, -10.0f * t) * MathF.Sin(((t * d) - s) * (2.0f * MathF.PI) / p)) + c + b;
    }

    /// <summary> Ease: Elastic In Out </summary>
    public static float EaseElasticInOut(float t, float b, float c, float d)
    {
        if (t == 0.0f)
        {
            return b;
        }

        if ((t /= d / 2.0f) == 2.0f)
        {
            return b + c;
        }

        float p = d * (0.3f * 1.5f);
        float a = c;
        float s = p / 4.0f;

        if (t < 1.0f)
        {
            float postFix = a * MathF.Pow(2.0f, 10.0f * (t -= 1.0f));
            return (-0.5f * (postFix * MathF.Sin(((t * d) - s) * (2.0f * MathF.PI) / p))) + b;
        }
        else
        {
            float postFix = a * MathF.Pow(2.0f, -10.0f * (t -= 1.0f));
            return (postFix * MathF.Sin(((t * d) - s) * (2.0f * MathF.PI) / p) * 0.5f) + c + b;
        }
    }
}

/// <summary> Easing function </summary>
public delegate float EasingFunction(float t, float b, float c, float d);

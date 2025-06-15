    using ColorTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ColorTrainer.Services
{
    // Перечисление для типов гармоний, которые мы будем использовать
    public enum HarmonyType
    {
        Complementary, // Комплементарная (противоположная)
        Analogous,     // Аналоговая (соседняя)
        Triadic,       // Триада (треугольник)
        Monochromatic  // Монохромная (оттенок)
    }

    public static class ColorHarmonyService
    {
        private static Random _random = new Random();

        // Главный метод, который будет вызывать наш уровень
        public static (ColorInfo CorrectColor, HarmonyType Type) GetHarmony(ColorInfo baseColor)
        {
            var harmonyTypes = Enum.GetValues(typeof(HarmonyType)).Cast<HarmonyType>().ToList();
            var randomHarmonyType = harmonyTypes[_random.Next(harmonyTypes.Count)];

            Color targetHarmonyColor;

            // Вычисляем нужный цвет в зависимости от правила
            switch (randomHarmonyType)
            {
                case HarmonyType.Complementary:
                    targetHarmonyColor = GetComplementary(baseColor.Brush.Color);
                    break;
                case HarmonyType.Analogous:
                    targetHarmonyColor = GetAnalogous(baseColor.Brush.Color);
                    break;
                case HarmonyType.Triadic:
                    targetHarmonyColor = GetTriadic(baseColor.Brush.Color);
                    break;
                case HarmonyType.Monochromatic:
                    targetHarmonyColor = GetMonochromatic(baseColor.Brush.Color);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Ищем в нашей базе данных наиболее близкий цвет к вычисленному
            var closestMatch = FindClosestColorInDatabase(targetHarmonyColor);

            return (closestMatch, randomHarmonyType);
        }

        // --- Логика вычисления гармоний ---

        private static Color GetComplementary(Color color)
        {
            var hsl = RgbToHsl(color);
            hsl.h = (hsl.h + 180) % 360; // Поворачиваем на 180 градусов по цветовому кругу
            return HslToRgb(hsl);
        }

        private static Color GetAnalogous(Color color)
        {
            var hsl = RgbToHsl(color);
            int spin = _random.Next(2) == 0 ? 30 : -30; // Берем соседа слева или справа
            hsl.h = (hsl.h + spin + 360) % 360;
            return HslToRgb(hsl);
        }

        private static Color GetTriadic(Color color)
        {
            var hsl = RgbToHsl(color);
            int spin = _random.Next(2) == 0 ? 120 : -120; // Поворачиваем на 120 градусов
            hsl.h = (hsl.h + spin + 360) % 360;
            return HslToRgb(hsl);
        }

        private static Color GetMonochromatic(Color color)
        {
            var hsl = RgbToHsl(color);
            // Делаем цвет либо светлее, либо темнее, но не слишком сильно
            double lightnessChange = _random.Next(2) == 0 ? 0.2 : -0.2;
            hsl.l = Math.Max(0.1, Math.Min(0.9, hsl.l + lightnessChange));
            return HslToRgb(hsl);
        }

        // --- Поиск ближайшего цвета в нашей базе ---
        private static ColorInfo FindClosestColorInDatabase(Color targetColor)
        {
            return ColorDatabase.GetColors()
                .OrderBy(c => ColorDistance(c.Brush.Color, targetColor))
                .First();
        }

        // Вычисление "расстояния" между двумя цветами в пространстве RGB
        private static double ColorDistance(Color c1, Color c2)
        {
            int rDiff = c1.R - c2.R;
            int gDiff = c1.G - c2.G;
            int bDiff = c1.B - c2.B;
            return Math.Sqrt(rDiff * rDiff + gDiff * gDiff + bDiff * bDiff);
        }


        #region HSL/RGB Converters
        // --- Вспомогательные методы для конвертации RGB <-> HSL ---

        private static (double h, double s, double l) RgbToHsl(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;
            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double h = 0, s = 0, l = (max + min) / 2;

            if (max == min)
            {
                h = s = 0; // achromatic
            }
            else
            {
                double d = max - min;
                s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
                if (max == r) h = (g - b) / d + (g < b ? 6 : 0);
                else if (max == g) h = (b - r) / d + 2;
                else if (max == b) h = (r - g) / d + 4;
                h /= 6;
            }
            return (h * 360, s, l);
        }

        private static Color HslToRgb((double h, double s, double l) hsl)
        {
            double r, g, b;
            double h = hsl.h / 360.0;

            if (hsl.s == 0)
            {
                r = g = b = hsl.l; // achromatic
            }
            else
            {
                Func<double, double, double, double> hue2rgb = (p, q, t) =>
                {
                    if (t < 0) t += 1;
                    if (t > 1) t -= 1;
                    if (t < 1.0 / 6) return p + (q - p) * 6 * t;
                    if (t < 1.0 / 2) return q;
                    if (t < 2.0 / 3) return p + (q - p) * (2.0 / 3 - t) * 6;
                    return p;
                };
                double q = hsl.l < 0.5 ? hsl.l * (1 + hsl.s) : hsl.l + hsl.s - hsl.l * hsl.s;
                double p = 2 * hsl.l - q;
                r = hue2rgb(p, q, h + 1.0 / 3);
                g = hue2rgb(p, q, h);
                b = hue2rgb(p, q, h - 1.0 / 3);
            }
            return Color.FromRgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        #endregion
    }
}
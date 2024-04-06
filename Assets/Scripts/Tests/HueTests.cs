using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Tests
{
    public class HueTests
    {

        [TestCase(3), TestCase(4), TestCase(5)]
        public void ColorGeneratorGeneratesCorrectNumberOfColors(int count)
        {
            // when
            var result = HueHelper.GenerateEvenlyDistributedColors(count);

            // then
            Assert.That(result.Length, Is.EqualTo(count));
        }

        [TestCase(3), TestCase(4), TestCase(5)]
        public void ColorGeneratorGeneratesEvenlyDistributedColors(int count)
        {
            // when
            var result = HueHelper.GenerateEvenlyDistributedColors(count);
            float expectedHueDistance = 1f / count;

            for (int i = 0; i < count; i++)
            {
                Color.RGBToHSV(result[i], out float hue1, out _, out _);
                Color.RGBToHSV(result[(i + 1) % count], out float hue2, out _, out _);
                var distance = Mathf.Min(Mathf.Abs(hue1 - hue2), Mathf.Abs(hue1 - hue2 - 1),
                    Mathf.Abs(hue1 - hue2 + 1));
                Assert.AreEqual(expectedHueDistance, distance, 0.01 * expectedHueDistance);
            }
        }

        [Test]
        public void HueMovesTowardsTarget()
        {
            // given
            float currentHue = 0.2f;
            float targetHue = 0.6f;
            float maxDistance = 0.15f;
            // when
            float result = HueHelper.MoveTowards(currentHue, targetHue, maxDistance);
            // that
            Assert.That(result, Is.EqualTo(0.35f));
        }
        
        [Test]
        public void HueMovesTowardsTargetInClosestDirection()
        {
            // given
            float currentHue = 0.2f;
            float targetHue = 0.9f;
            float maxDistance = 0.15f;
            // when
            float result = HueHelper.MoveTowards(currentHue, targetHue, maxDistance);
            // that
            Assert.That(result, Is.EqualTo(0.05f));
        }
        
        [Test]
        public void HueUnderflows()
        {
            // given
            float currentHue = 0.05f;
            float targetHue = 0.8f;
            float maxDistance = 0.2f;
            // when
            float result = HueHelper.MoveTowards(currentHue, targetHue, maxDistance);
            // that
            Assert.That(result, Is.EqualTo(0.85f));
        }
        
        [Test]
        public void HueOverflows()
        {
            // given
            float currentHue = 0.8f;
            float targetHue = 0.05f;
            float maxDistance = 0.2f;
            // when
            float result = HueHelper.MoveTowards(currentHue, targetHue, maxDistance);
            // that
            Assert.That(result, Is.EqualTo(1f));
        }
        
        [Test]
        public void HueDoesNotMovePastTarget()
        {
            // given
            float currentHue = 0.8f;
            float targetHue = 0.85f;
            float maxDistance = 0.2f;
            // when
            float result = HueHelper.MoveTowards(currentHue, targetHue, maxDistance);
            // that
            Assert.That(result, Is.EqualTo(0.85f));
        }
    }
}
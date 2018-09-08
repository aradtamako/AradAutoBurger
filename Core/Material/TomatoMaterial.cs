using System.Collections.Generic;
using System.Drawing;

namespace Core.Material
{
    /// <summary>
    /// トマト
    /// </summary>
    public class TomatoMaterial : IMaterial
    {
        public Point TeacherCordinate { get; } = new Point(63, 214);
        public List<Color> Material { get; set; } = new List<Color>();
    }
}

using System.Collections.Generic;
using System.Drawing;

namespace Core.Material
{
    /// <summary>
    /// レタス
    /// </summary>
    public class LettuceMaterial : IMaterial
    {
        public Point TeacherCordinate { get; } = new Point(63, 199);
        public List<Color> Material { get; set; } = new List<Color>();
    }
}

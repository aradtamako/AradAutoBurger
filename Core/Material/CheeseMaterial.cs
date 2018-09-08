using System.Collections.Generic;
using System.Drawing;

namespace Core.Material
{
    /// <summary>
    /// チーズ
    /// </summary>
    public class CheeseMaterial : IMaterial
    {
        public Point TeacherCordinate { get; } = new Point(53, 177);
        public List<Color> Material { get; set; } = new List<Color>();
    }
}

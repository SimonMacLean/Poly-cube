using cubething.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cubething
{
    public partial class Form1 : Form
    {
        PuzzlePiece original;
        Timer t;
        public Form1()
        {
            InitializeComponent();
            typeof(Form).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });
            original = new PuzzlePiece(new Cube(0, 0, 0), new Cube(1, 0, 0), new Cube(2, 0, 0), new Cube(3, 0, 0), new Cube(0, 1, 0), new Cube(0, 2, 0), new Cube(0, 3, 0), new Cube(0, 0, 1), new Cube(0, 0, 2), new Cube(0, 0, 3));
            t = new Timer();
            t.Enabled = true;
            t.Interval = 1;
            t.Tick += Draw;
        }

        private void Draw(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            original.Draw(e.Graphics);
        }

        private void RotateX_Click(object sender, EventArgs e)
        {
            original = original.XRotate90.Center;
        }

        private void RotateY_Click(object sender, EventArgs e)
        {
            original = original.YRotate90.Center;
        }

        private void RotateZ_Click(object sender, EventArgs e)
        {
            original = original.ZRotate90.Center;
        }
    }
    public struct Point3D
    {
        public int X, Y, Z;
        public Point RealPoint { get { return new Point((int)(((double)X - Z + 5) * 50), (int)((-1.2 * Y + 0.6 * X + 0.6 * Z + 5) * 50)); } }
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static bool operator ==(Point3D a, Point3D b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static bool operator !=(Point3D a, Point3D b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public struct Edge3D
    {
        Point3D A, B;
        public Edge3D(Point3D a, Point3D b)
        {
            A = a;
            B = b;
        }
        public void Draw(Graphics G)
        {
            G.DrawLine(Pens.Black, A.RealPoint, B.RealPoint);
        }
    }
    public struct Face3D
    {
        public static bool operator ==(Face3D A, Face3D B)
        {
            return A.Verticies.OrderBy(t => t.Z) == B.Verticies.OrderBy(p => p.Z);
        }
        public static bool operator !=(Face3D A, Face3D B)
        {
            return A.Verticies.OrderBy(t => t.Z) != B.Verticies.OrderBy(p => p.Z);
        }
        public List<Point3D> Verticies;
        public List<Edge3D> Edges;
        public Face3D(params Point3D[] Verticies)
        {
            this.Verticies = new List<Point3D>();
            Edges = new List<Edge3D>();
            for (int i = 0; i < Verticies.Length; i++)
            {
                this.Verticies.Add(Verticies[i]);
                Edges.Add(new Edge3D(Verticies[(i + Verticies.Length - 1) % Verticies.Length], Verticies[i]));
            }
        }
        public void Draw(Graphics G)
        {
            Point[] realPoints = new Point[Verticies.Count];
            for (int i = 0; i < Verticies.Count; i++)
            {
                realPoints[i] = Verticies[i].RealPoint;
            }
            G.FillPolygon(Brushes.Gray, realPoints);
            foreach (Edge3D e in Edges)
            {
                e.Draw(G);
            }
        }
        /*public static bool operator ==(Face3D a, Face3D b)
        {
            List<Point3D> points = new List<Point3D>();
            foreach (Point3D p in a.Verticies)
            {
                points.Add(p);
            }
            foreach (Point3D p in b.Verticies)
            {
                points.Add(p);
            }
            while(points.Count > 0)
            {
                Point3D p = points.First();
                points.Remove(p);
                if (!points.Contains(p))
                    return false;
                points.Remove(p);
            }
            return true;
        }
        public static bool operator !=(Face3D a, Face3D b)
        {
            return !(a == b);
        }*/
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public struct Cube
    {
        public static bool operator ==(Cube A, Cube B)
        {
            return A.Location == B.Location;
        }
        public static bool operator !=(Cube A, Cube B)
        {
            return A.Location != B.Location;
        }
        public Point3D Location;
        public Face3D[] Faces; // top, left, front
        public Face3D[] VisibleFaces; //top, back, left, front, right, bottom
        public int DistanceX
        {
            get
            {
                return Math.Abs(Location.X);
            }
        }
        public int DistanceY
        {
            get
            {
                return Math.Abs(Location.Y);
            }
        }
        public int DistanceZ
        {
            get
            {
                return Math.Abs(Location.Z);
            }
        }
        public Cube(Point3D location)
        {
            Location = location;
            Faces = new Face3D[6];
            int Y = location.Y;
            int X = location.X;
            int Z = location.Z;
            Faces[0] = new Face3D(new Point3D(X, Y, Z - 1), new Point3D(X + 1, Y, Z - 1), new Point3D(X + 1, Y, Z), location);
            Faces[1] = new Face3D(new Point3D(X + 1, Y, Z - 1), new Point3D(X, Y, Z - 1), new Point3D(X, Y - 1, Z - 1), new Point3D(X, Y - 1, Z - 1));
            Faces[2] = new Face3D(new Point3D(X, Y, Z - 1), new Point3D(X, Y, Z), new Point3D(X, Y - 1, Z), new Point3D(X, Y - 1, Z - 1));
            Faces[3] = new Face3D(new Point3D(X, Y, Z), new Point3D(X + 1, Y, Z), new Point3D(X + 1, Y - 1, Z), new Point3D(X, Y - 1, Z));
            Faces[4] = new Face3D(new Point3D(X + 1, Y, Z), new Point3D(X + 1, Y, Z - 1), new Point3D(X + 1, Y - 1, Z - 1), new Point3D(X + 1, Y - 1, Z));
            Faces[5] = new Face3D(new Point3D(X, Y - 1, Z), new Point3D(X + 1, Y, Z), new Point3D(X + 1, Y - 1, Z - 1), new Point3D(X, Y - 1, Z - 1));
            VisibleFaces = new Face3D[3];
            VisibleFaces[0] = Faces[0];
            VisibleFaces[1] = Faces[2];
            VisibleFaces[2] = Faces[3];
        }
        public Cube(int x, int y, int z)
        {
            Location = new Point3D(x, y, z);
            Faces = new Face3D[6];
            int Y = Location.Y;
            int X = Location.X;
            int Z = Location.Z;
            Faces[0] = new Face3D(new Point3D(X, Y, Z - 1), new Point3D(X + 1, Y, Z - 1), new Point3D(X + 1, Y, Z), Location);
            Faces[1] = new Face3D(new Point3D(X + 1, Y, Z - 1), new Point3D(X, Y, Z - 1), new Point3D(X, Y - 1, Z - 1), new Point3D(X, Y - 1, Z - 1));
            Faces[2] = new Face3D(new Point3D(X, Y, Z - 1), new Point3D(X, Y, Z), new Point3D(X, Y - 1, Z), new Point3D(X, Y - 1, Z - 1));
            Faces[3] = new Face3D(new Point3D(X, Y, Z), new Point3D(X + 1, Y, Z), new Point3D(X + 1, Y - 1, Z), new Point3D(X, Y - 1, Z));
            Faces[4] = new Face3D(new Point3D(X + 1, Y, Z), new Point3D(X + 1, Y, Z - 1), new Point3D(X + 1, Y - 1, Z - 1), new Point3D(X + 1, Y - 1, Z));
            Faces[5] = new Face3D(new Point3D(X, Y - 1, Z), new Point3D(X + 1, Y, Z), new Point3D(X + 1, Y - 1, Z - 1), new Point3D(X, Y - 1, Z - 1));
            VisibleFaces = new Face3D[3];
            VisibleFaces[0] = Faces[0];
            VisibleFaces[1] = Faces[2];
            VisibleFaces[2] = Faces[3];
        }
        public void Draw(Graphics G)
        {
            //foreach (Face3D f in Faces)
            //{
            //    f.Draw(G);
            //}
            G.DrawImage(Resources.Cube, Location.RealPoint);
        }
    }
    public struct PuzzlePiece
    {
        public Point3D dimensions;
        public List<Cube> Cubes;
        public List<Face3D> OpenFaces;
        public PuzzlePiece Center
        {
            get
            {
                Point3D offset = new Point3D(100, 100, 100);
                foreach (Cube c in Cubes)
                {
                    if (c.Location.X < offset.X)
                        offset.X = c.Location.X;
                    if (c.Location.Y < offset.Y)
                        offset.Y = c.Location.Y;
                    if (c.Location.Z < offset.Z)
                        offset.Z = c.Location.Z;
                }
                List<Cube> centeredCubes = new List<Cube>();
                foreach (Cube c in Cubes)
                {
                    centeredCubes.Add(new Cube(c.Location.X - offset.X, c.Location.Y - offset.Y, c.Location.Z - offset.Z));
                }
                return new PuzzlePiece(centeredCubes);
            }
        }

        public PuzzlePiece XRotate90
        {
            get
            {
                List<Cube> rotatedCubes = new List<Cube>();
                foreach (Cube c in Cubes)
                {
                    int x = c.Location.Y;
                    int y = c.Location.Z;
                    int offset = dimensions.Y;
                    rotatedCubes.Add(new Cube(new Point3D(c.Location.X, y + offset, -x)));
                }
                return new PuzzlePiece(rotatedCubes);
            }
        }
        public PuzzlePiece YRotate90
        {
            get
            {
                List<Cube> rotatedCubes = new List<Cube>();
                foreach (Cube c in Cubes)
                {
                    int x = c.Location.Z;
                    int y = c.Location.X;
                    int offset = dimensions.Z;
                    rotatedCubes.Add(new Cube(new Point3D(-x, c.Location.Y, y + offset)));
                }
                return new PuzzlePiece(rotatedCubes);
            }
        }
        public PuzzlePiece ZRotate90
        {
            get
            {
                var rotatedCubes = new List<Cube>();
                foreach (Cube c in Cubes)
                {
                    int x = c.Location.X;
                    int y = c.Location.Y;
                    int offset = dimensions.X;
                    rotatedCubes.Add(new Cube(new Point3D(y + offset, -x, c.Location.Z)));
                }
                return new PuzzlePiece(rotatedCubes);
            }
        }
        public PuzzlePiece(List<Cube> cubes)
        {
            Cubes = cubes;
            dimensions = new Point3D(Cubes.OrderBy(c => -c.DistanceX).FirstOrDefault().Location.X, Cubes.OrderBy(c => -c.DistanceY).FirstOrDefault().Location.Y, Cubes.OrderBy(c => -c.DistanceZ).FirstOrDefault().Location.Z);
            OpenFaces = new List<Face3D>();
            foreach (Cube c in Cubes)
            {
                foreach (Face3D f in c.Faces)
                {
                    OpenFaces.Add(f);
                }
            }
            OpenFaces = new List<Face3D>(OpenFaces.Distinct());
        }
        public PuzzlePiece(params Cube[] cubes)
        {
            Cubes = new List<Cube>();
            foreach (Cube c in cubes)
            {
                Cubes.Add(c);
            }
            dimensions = new Point3D(Cubes.OrderBy(c => -c.DistanceX).FirstOrDefault().Location.X, Cubes.OrderBy(c => -c.DistanceY).FirstOrDefault().Location.Y, Cubes.OrderBy(c => -c.DistanceZ).FirstOrDefault().Location.Z);
            OpenFaces = new List<Face3D>();
            foreach (Cube c in Cubes)
            {
                foreach (Face3D f in c.Faces)
                {
                    OpenFaces.Add(f);
                }
            }
            foreach (Face3D f in OpenFaces)
            {

            }
        }
        public void Draw(Graphics G)
        {
            var ordered = Cubes.OrderBy(c => c.DistanceY);
            ordered = ordered.OrderBy(c => c.DistanceX);
            ordered = ordered.OrderBy(c => c.DistanceZ);
            foreach (var c in ordered)
            {
                c.Draw(G);
            }
        }
    }
}

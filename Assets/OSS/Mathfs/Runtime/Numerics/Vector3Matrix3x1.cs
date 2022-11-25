// by Freya Holmér (https://github.com/FreyaHolmer/Mathfs)
// Do not manually edit - this file is generated by MathfsCodegen.cs

using System;
using UnityEngine;
namespace Freya {
	/// <summary>A 3x1 column matrix with Vector3 values</summary>
	[Serializable] public struct Vector3Matrix3x1 {
		public Vector3 m0, m1, m2;
		public Vector3Matrix3x1(Vector3 m0, Vector3 m1, Vector3 m2) => (this.m0, this.m1, this.m2) = (m0, m1, m2);
		public Vector3Matrix3x1(Matrix3x1 x, Matrix3x1 y, Matrix3x1 z) => (m0, m1, m2) = (new Vector3(x.m0, y.m0, z.m0), new Vector3(x.m1, y.m1, z.m1), new Vector3(x.m2, y.m2, z.m2));
		public Vector3 this[int row] {
			get => row switch{0 => m0, 1 => m1, 2 => m2, _ => throw new IndexOutOfRangeException( $"Matrix row index has to be from 0 to 2, got: {row}" )};
			set {
				switch(row) {
					case 0: m0 = value; break; case 1: m1 = value; break; case 2: m2 = value; break;
					default: throw new IndexOutOfRangeException( $"Matrix row index has to be from 0 to 2, got: {row}" );
				}
			}
		}
		public Matrix3x1 X => new(m0.x, m1.x, m2.x);
		public Matrix3x1 Y => new(m0.y, m1.y, m2.y);
		public Matrix3x1 Z => new(m0.z, m1.z, m2.z);
		/// <summary>Linearly interpolates between two matrices, based on a value <c>t</c></summary>
		/// <param name="t">The value to blend by</param>
		public static Vector3Matrix3x1 Lerp( Vector3Matrix3x1 a, Vector3Matrix3x1 b, float t ) => new Vector3Matrix3x1(Vector3.LerpUnclamped( a.m0, b.m0, t ), Vector3.LerpUnclamped( a.m1, b.m1, t ), Vector3.LerpUnclamped( a.m2, b.m2, t ));
		public static bool operator ==( Vector3Matrix3x1 a, Vector3Matrix3x1 b ) => a.m0 == b.m0 && a.m1 == b.m1 && a.m2 == b.m2;
		public static bool operator !=( Vector3Matrix3x1 a, Vector3Matrix3x1 b ) => !( a == b );
		public bool Equals( Vector3Matrix3x1 other ) => m0.Equals( other.m0 ) && m1.Equals( other.m1 ) && m2.Equals( other.m2 );
		public override bool Equals( object obj ) => obj is Vector3Matrix3x1 other && Equals( other );
		public override int GetHashCode() => HashCode.Combine( m0, m1, m2 );
	}
}

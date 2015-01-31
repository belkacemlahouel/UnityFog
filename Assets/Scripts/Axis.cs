using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class Axis {

	private Vector2 origin, direction;
	private float m, p; // y = mx + p

	public Axis(Vector2 origin, Vector2 direction) {
		this.origin = origin;
		this.direction = direction;

		m = direction.y/direction.x;
		p = origin.y - origin.x*m;
	}

	public override string ToString() {
		if (isXConstant())
			return (new StringBuilder("x = ")).Append(x()).ToString();

		StringBuilder builder = new StringBuilder("y = ");
		builder.Append(m).Append("*x + ").Append(p);
		return builder.ToString();
	}

	public bool isXConstant() {
		return (Double.IsInfinity(m) || Double.IsNaN(m) || Double.IsNaN(p));
	}

	public float x() {
		if (isXConstant())
			return origin.x;
		throw new ArgumentException("This axis is not x-constant.");
	}

	public float y(float x) {
		return m*x + p;
	}

	public static Vector2 intersection(Axis a1, Axis a2) {
		if (a1.isXConstant())
			return new Vector2(a1.x(), a2.y(a1.x()));

		if (a2.isXConstant())
			return new Vector2(a2.x(), a1.y(a2.x()));

		float x, y;
		x = (a2.p-a1.p)/(a1.m-a2.m);
		y = a1.y(x);
		// if (a2.y(x) != y)
		// 	throw new ArgumentException(a2.y(x) + " != " + y);
		return new Vector2(x, y);
	}
}

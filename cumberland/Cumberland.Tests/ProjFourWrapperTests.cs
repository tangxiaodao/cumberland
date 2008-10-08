// ProjFourWrapperTests.cs
//
// Copyright (c) 2008 Scott Ellington and Authors
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//

using System;
using NUnit.Framework;

using Cumberland;

namespace Cumberland.Tests
{
	
	[TestFixture]
	public class ProjFourWrapperTests
	{
		[Test]
		public void TestConvertFromLatLong()
		{
			using (ProjFourWrapper proj = new ProjFourWrapper("+init=epsg:2236"))
			{
				Point pt = proj.ConvertFromLatLong(new Point(-81, 26));
				pt.X = Math.Round(pt.X, 2);
				pt.Y = Math.Round(pt.Y, 2);

				// used proj command line to acquire this
				Assert.AreEqual(new Point(656166.67, 605690.54), pt);				
			}
		}
		
		[Test]
		public void TestConvertToLatLong()
		{
			using (ProjFourWrapper proj = new ProjFourWrapper("+init=epsg:2236"))
			{
				Point pt = proj.ConvertToLatLong(new Point(656166.67, 605690.54));
				pt.X = Math.Round(pt.X, 2);
				pt.Y = Math.Round(pt.Y, 2);
				
				// used proj command line to acquire this
				Assert.AreEqual(new Point(-81, 26), pt);				
			}
		}
		
		[Test]
		public void TestTransformPointFromCSToLL()
		{
			using (ProjFourWrapper src = new ProjFourWrapper("+init=epsg:2236"))
			{
				using (ProjFourWrapper dst = new ProjFourWrapper("+init=epsg:4326"))
				{
					Point pt = src.Transform(dst, new Point(656166.67, 605690.54));
					pt.X = Math.Round(pt.X);
					pt.Y = Math.Round(pt.Y);
					
					// used proj command line to acquire this
					Assert.AreEqual(new Point(-81, 26), pt);
				}
			}
		}
		
		[Test]
		public void TestTransformPointFromLLToCS()
		{
			using (ProjFourWrapper src = new ProjFourWrapper("+init=epsg:4326"))
			{
				using (ProjFourWrapper dst = new ProjFourWrapper("+init=epsg:2236"))
				{
					Point pt = src.Transform(dst, new Point(-81, 26));
					pt.X = Math.Round(pt.X, 2);
					pt.Y = Math.Round(pt.Y, 2);
					
					// used proj command line to acquire this
					Assert.AreEqual(new Point(656166.67, 605690.54), pt);
				}
			}
		}
		
		[Test]
		public void TestTransformPointFromLLToLL()
		{
			using (ProjFourWrapper src = new ProjFourWrapper("+init=epsg:4326"))
			{
				using (ProjFourWrapper dst = new ProjFourWrapper("+init=epsg:4269"))
				{
					Point pt1 = new Point(-81, 26);
					Point pt2 = src.Transform(dst, pt1);

					// NAD83 and WGS84 should be essentially same
					Assert.AreEqual(pt1, pt2);
				}
			}
		}
		
		[Test]
		public void TestTransformPointFromCSToCS()
		{
			using (ProjFourWrapper src = new ProjFourWrapper("+init=epsg:2236"))
			{
				using (ProjFourWrapper dst = new ProjFourWrapper("+init=epsg:3086"))
				{
					Point pt = src.Transform(dst,  new Point(656166.67, 605690.54));
					pt.X = Math.Round(pt.X, 2);
					pt.Y = Math.Round(pt.Y, 2);

					// used cs2cs command line to acquire this
					Assert.AreEqual(new Point(699831.04, 225395.97), pt);
				}
			}
		}
	}
}
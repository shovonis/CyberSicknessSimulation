﻿/******************************************************************************
 *
 * The MIT License (MIT)
 *
 * MIConvexHull, Copyright (c) 2015 David Sehnal, Matthew Campbell
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *  
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace DWP2MiConvexHull
{
    /// <summary>
    /// Factory class for computing convex hulls.
    /// </summary>
    public static class ConvexHull
    {
        /// <summary>
        /// Creates a convex hull of the input data.
        /// </summary>
        /// <typeparam name="TVertex">The type of the t vertex.</typeparam>
        /// <typeparam name="TFace">The type of the t face.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="PlaneDistanceTolerance">The plane distance tolerance.</param>
        /// <returns>
        /// ConvexHull&lt;TVertex, TFace&gt;.
        /// </returns>
        public static ConvexHull<TVertex, TFace> Create<TVertex, TFace>(IList<TVertex> data,
            double PlaneDistanceTolerance = Constants.DefaultPlaneDistanceTolerance)
            where TVertex : IVertex
            where TFace : ConvexFace<TVertex, TFace>, new()
        {
            return ConvexHull<TVertex, TFace>.Create(data, PlaneDistanceTolerance);
        }

        /// <summary>
        /// Creates a convex hull of the input data.
        /// </summary>
        /// <typeparam name="TVertex">The type of the t vertex.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="PlaneDistanceTolerance">The plane distance tolerance.</param>
        /// <returns>
        /// ConvexHull&lt;TVertex, DefaultConvexFace&lt;TVertex&gt;&gt;.
        /// </returns>
        public static ConvexHull<TVertex, DefaultConvexFace<TVertex>> Create<TVertex>(IList<TVertex> data,
            double PlaneDistanceTolerance = Constants.DefaultPlaneDistanceTolerance)
            where TVertex : IVertex
        {
            return ConvexHull<TVertex, DefaultConvexFace<TVertex>>.Create(data, PlaneDistanceTolerance);
        }

        /// <summary>
        /// Creates a convex hull of the input data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="PlaneDistanceTolerance">The plane distance tolerance.</param>
        /// <returns>
        /// ConvexHull&lt;DefaultVertex, DefaultConvexFace&lt;DefaultVertex&gt;&gt;.
        /// </returns>
        public static ConvexHull<DefaultVertex, DefaultConvexFace<DefaultVertex>> Create(IList<double[]> data,
            double PlaneDistanceTolerance = Constants.DefaultPlaneDistanceTolerance)
        {
            var points = data.Select(p => new DefaultVertex { Position = p }).ToList();
            return ConvexHull<DefaultVertex, DefaultConvexFace<DefaultVertex>>.Create(points, PlaneDistanceTolerance);
        }
    }

    /// <summary>
    /// Representation of a convex hull.
    /// </summary>
    /// <typeparam name="TVertex">The type of the t vertex.</typeparam>
    /// <typeparam name="TFace">The type of the t face.</typeparam>
    public class ConvexHull<TVertex, TFace>
        where TVertex : IVertex
        where TFace : ConvexFace<TVertex, TFace>, new()
    {
        /// <summary>
        /// Can only be created using a factory method.
        /// </summary>
        internal ConvexHull()
        {
        }

        /// <summary>
        /// Points of the convex hull.
        /// </summary>
        /// <value>The points.</value>
        public IEnumerable<TVertex> Points { get; internal set; }

        /// <summary>
        /// Faces of the convex hull.
        /// </summary>
        /// <value>The faces.</value>
        public IEnumerable<TFace> Faces { get; internal set; }

        /// <summary>
        /// Creates the convex hull.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="PlaneDistanceTolerance">The plane distance tolerance.</param>
        /// <returns>
        /// ConvexHull&lt;TVertex, TFace&gt;.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">The supplied data is null.</exception>
        /// <exception cref="ArgumentNullException">data</exception>
        public static ConvexHull<TVertex, TFace> Create(IList<TVertex> data, double PlaneDistanceTolerance)
        {
            if (data == null) throw new ArgumentNullException("The supplied data is null.");
            return ConvexHullAlgorithm.GetConvexHull<TVertex, TFace>(data, PlaneDistanceTolerance);
        }
    }
}
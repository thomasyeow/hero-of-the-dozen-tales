﻿namespace Ink.Runtime
{
    /// <summary>
    /// Internal structure used to point to a particular / current point in the story.
    /// Where Path is a set of components that make content fully addressable, this is
    /// a reference to the current container, and the index of the current piece of 
    /// content within that container. This scheme makes it as fast and efficient as
    /// possible to increment the pointer (move the story forwards) in a way that's as
    /// native to the internal engine as possible.
    /// </summary>
	public struct Pointer
    {
        public Container container;
        public int index;

        public Pointer(Container container, int index)
        {
            this.container = container;
            this.index = index;
        }

        public Runtime.Object Resolve()
        {
            return index < 0
                ? container
                : container == null
                ? null
                : container.content.Count == 0 ? container : index >= container.content.Count ? null : container.content[index];
        }

        public bool isNull
        {
            get
            {
                return container == null;
            }
        }

        public Path path
        {
            get
            {
                return isNull ? null : index >= 0 ? container.path.PathByAppendingComponent(new Path.Component(index)) : container.path;
            }
        }

        public override string ToString()
        {
            return container == null ? "Ink Pointer (null)" : "Ink Pointer -> " + container.path.ToString() + " -- index " + index;
        }

        public static Pointer StartOf(Container container)
        {
            return new Pointer
            {
                container = container,
                index = 0
            };
        }

        public static Pointer Null = new Pointer { container = null, index = -1 };

    }
}
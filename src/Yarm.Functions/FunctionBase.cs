using System;

namespace Yarm.Functions
{
    /// <summary>
    /// This represents the base function entity.
    /// </summary>
    public abstract class FunctionBase : IFunction
    {
        private bool _disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Value indicating whether to dispose managed resources or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                this.ReleaseManagedResources();
            }

            this.ReleaseUnmanagedResources();
        }

        private void ReleaseManagedResources()
        {
            // Release managed resources here.
        }

        private void ReleaseUnmanagedResources()
        {
            // Release unmanaged resources here.
        }
    }
}
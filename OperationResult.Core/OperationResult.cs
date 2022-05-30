namespace OperationResult.Core
{
    using Models;
    
    // ReSharper disable IntroduceOptionalParameters.Global
    // ReSharper disable UnusedMember.Global

    /// <summary>
    /// Any action result
    /// </summary>
    [Serializable]
    public abstract class OperationResult
    {
        /// <summary>
        /// Operation result metadata
        /// </summary>
        public Metadata? Metadata { get; set; }

        /// <summary>
        /// Exception that occurred during execution
        /// </summary>
        public Exception? Exception { get; set; }
        
        /// <summary>
        /// Returns True when Exception == null when Metadata property == null.
        /// Otherwise, when Metadata != null then validation works as 
        /// Exception equals NULL and Result not equals NULL and Metadata type not equals MetadataError and MetadataWarning.
        /// </summary>
        public virtual bool Ok
        {
            get
            {
                if (Metadata == null)
                {
                    return Exception == null;
                }
                return Exception == null
                       && Metadata?.Type != MetadataType.Error;
            }
        }
    }

    /// <summary>
    /// Generic operation result for any requests for Web API service and some MVC actions.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    [Serializable]
    public class OperationResult<TData> : OperationResult
    {
        /// <summary>
        /// Result for server operation
        /// </summary>
        public TData? Result { get; set; }

        public override bool Ok => base.Ok && Result != null;

        
        public static implicit operator OperationResult<TData>(string message)
        {
            return OperationResultFactory.CreateResult<TData>(message);
        }

        public static implicit operator OperationResult<TData>(TData data)
        {
            return OperationResultFactory.CreateResult(data);
        }
    }
}
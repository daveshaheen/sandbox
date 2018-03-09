using ProtoBuf;

namespace App.Models
{
    /// <summary>Response</summary>
    [ProtoContract]
    public class Response
    {
        /// <summary>Gets or sets the errors, if there are any. Can be null.</summary>
        [ProtoMember(1)]
        public string[] Errors { get; set; }

        /// <summary>Gets or sets the parking rate price.</summary>
        /// <remarks>
        ///     NOTE: I had thoughts of calling this property something generic like content which could be used to wrap whatever object was to be sent back to the client; then, add another property, Type, to describe the content type. I haven't had a chance to explore this idea yet, especially with using protocol buffers. Also, need to decide how to handle API versions. Maybe sticking Nginx in front of the API and let it route to different containers running different versions of API would be a good approach.
        /// </remarks>
        [ProtoMember(2)]
        public int? Price { get; set; }
    }
}

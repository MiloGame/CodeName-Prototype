using UnityEngine;
using System.Runtime.Serialization;

namespace Assets.Scripts
{
    public sealed class SerlizbleVector3 : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Vector3 v3 = (Vector3)obj;
            info.AddValue("x",v3.x);
            info.AddValue("y",v3.y);
            info.AddValue("z", v3.z);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Vector3 v3 = (Vector3)obj;
            v3.x = info.GetSingle("x");
            v3.y = info.GetSingle("y");
            v3.z = info.GetSingle("z");
            obj = v3;
            return obj;
        }
    }
}

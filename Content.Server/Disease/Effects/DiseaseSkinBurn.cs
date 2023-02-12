using Content.Shared.Disease;
using JetBrains.Annotations;
using Content.Shared.Damage;
using Robust.Shared.Audio;

namespace Content.Server.Disease
{
    /// <summary>
    /// Makes the diseased's skin burn from inside
    /// or neither.
    /// </summary>
    [UsedImplicitly]
    public sealed class DiseaseSkinBurn : DiseaseEffect
    {
        /// <summary>
        /// Message to play when burning
        /// </summary>
        [DataField("burnMessage")]
        public string burnMessage = "disease-skin-burn";

        /// <summary>
        /// Sound to play when burning
        /// </summary>
        [DataField("burnSound")]
        public SoundSpecifier? burnSound;


        /// <summary>
        /// Whether to spread the disease through the air
        /// </summary>
        [DataField("airTransmit")]
        public bool AirTransmit = true;


        public override void Effect(DiseaseEffectArgs args)
        {
            EntitySystem.Get<DiseaseSystem>().SneezeCough(args.DiseasedEntity, args.Disease, burnMessage, burnSound, AirTransmit);
        }
        
    }
}

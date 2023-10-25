public class EffectPacket
{
    public BaseEffect effect;
    public float elapsedTime;

    public EffectPacket(BaseEffect newEffect, float newTime)
    {
        effect = newEffect;
        elapsedTime = newTime;
    }
}

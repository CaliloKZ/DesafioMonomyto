using Photon.Pun;

public interface IDamageable
{
    public int Health { get; set; }
    public void Damage(int damageAmount);
    public void Damage(int damageAmount, PhotonView view);
}

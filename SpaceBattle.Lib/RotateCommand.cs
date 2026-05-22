namespace SpaceBattle.Lib;

public class RotateCommand : ICommand
{
    private readonly IRotatable _rotatable;

    public RotateCommand(IRotatable rotatable)
    {
        _rotatable = rotatable;
    }

    public void Execute()
    {
        Angle currentAngle = _rotatable.Angle;
        Angle velocity = _rotatable.AngularVelocity;

        _rotatable.Angle = currentAngle + velocity;
    }
}
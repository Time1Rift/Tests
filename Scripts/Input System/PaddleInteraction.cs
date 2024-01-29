using UnityEngine.InputSystem;

public class PaddleInteraction : IInputInteraction  //  �������� ������ Interaction ��� ����� input system
{
    public float Duration = 0.5f;

    [UnityEditor.InitializeOnLoadMethod]
    private static void Register()
    {
        InputSystem.RegisterInteraction<PaddleInteraction>();
    }

    public void Process(ref InputInteractionContext context)
    {
        if (context.timerHasExpired)
        {
            context.Canceled();
            return;
        }

        switch (context.phase)
        {
            case InputActionPhase.Waiting:  //  ��������
                if (context.ReadValue<float>() == 1)
                {
                    context.Started();
                    context.SetTimeout(Duration);
                }
                break;

            case InputActionPhase.Started:
                if (context.ReadValue<float>() == -1)
                {
                    context.Performed();
                }
                break;
        }
    }

    public void Reset() { } //  ��� ������ ���������
}
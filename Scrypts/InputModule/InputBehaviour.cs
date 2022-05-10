using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scrypts.InputModule
{
    public abstract class InputBehaviour : MonoBehaviour
    {
        //собыьте ввода символа
        [SerializeField] static UnityEvent<string> onSymbolInput = new UnityEvent<string>();
        public static void Subscribe(UnityAction<string> sub) => onSymbolInput.AddListener(sub);
        public static void UnSubscribe(UnityAction<string> sub) => onSymbolInput.RemoveListener(sub);
        //вызывается в наследниках при вводе символа
        protected void OnSymbolInput() => onSymbolInput.Invoke(InputSymbol().ToLower());
        protected abstract string InputSymbol();
    }
}

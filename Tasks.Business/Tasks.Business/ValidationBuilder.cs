namespace Tasks.Business
{
    public static class Validations {

        public static SingleValidationBuilder<TArg> SingleValidation<TArg>() {
            return new SingleValidationBuilder<TArg>();
        }

        public static ComposedValidationBuilder<TArg> ComposeValidations<TArg>() {
            return new ComposedValidationBuilder<TArg>();
        }
    }

    public interface IValidationBuilder<TArg>
    {
        void Evaluate(TArg arg);
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Tasks.Business
{

    public class ComposedValidationBuilder<TArg> :   IValidationBuilder<TArg>
    {
        private IList<Validation<TArg>> validations = new List<Validation<TArg>>();

        public ComposedValidationBuilder<TArg> Next(Validation<TArg> validation) {
            this.validations.Add(validation);
            return this;
        }

        public ComposedValidationBuilder<TArg> Next(IValidationBuilder<TArg> validation) {
            this.validations.Add(validation.Evaluate);
            return this;
        }

        public void Evaluate(TArg arg) {
            foreach (var validation in this.validations)
            {
                validation(arg);
            }
        }
    }
    
}
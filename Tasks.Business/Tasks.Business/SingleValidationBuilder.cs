using System;

namespace Tasks.Business
{
    public class SingleValidationBuilder<TArg> : IValidationBuilder<TArg>
    {
        private Predicate<TArg> predicate = (arg) => false;
        private Func<TArg, ValidationException> exceptionProvider;

        public SingleValidationBuilder<TArg> When(Predicate<TArg> predicate) {
            this.predicate = predicate;
            return this;
        }

        public SingleValidationBuilder<TArg> And(Predicate<TArg> predicate) {
            this.predicate = new DisjunctivePredicate(this.predicate, predicate).Evaluate;
            return this;
        }

        public SingleValidationBuilder<TArg> Or(Predicate<TArg> predicate) {
            this.predicate = new ConjunctivePredicate(this.predicate, predicate).Evaluate;
            return this;
        }

        public SingleValidationBuilder<TArg> Then(Func<TArg, ValidationException> exceptionProvider) {
            this.exceptionProvider = exceptionProvider;
            return this;
        }

        public void Evaluate(TArg arg) {
            if(this.predicate(arg)) {
                throw this.exceptionProvider(arg);
            }
        }

        protected class DisjunctivePredicate {
            private Predicate<TArg> leftOperand;
            private Predicate<TArg> rightOperand;

            public DisjunctivePredicate(Predicate<TArg> leftOperand, Predicate<TArg> rightOperand)
            {
                this.leftOperand = leftOperand;
                this.rightOperand = rightOperand;
            }

            public bool Evaluate(TArg arg) {
                return this.leftOperand(arg) && this.rightOperand(arg);
            }
        }
        
        protected class ConjunctivePredicate {
            private Predicate<TArg> leftOperand;
            private Predicate<TArg> rightOperand;

            public ConjunctivePredicate(Predicate<TArg> leftOperand, Predicate<TArg> rightOperand)
            {
                this.leftOperand = leftOperand;
                this.rightOperand = rightOperand;
            }

            public bool Evaluate(TArg arg) {
                return this.leftOperand(arg) || this.rightOperand(arg);
            }
        }
    }
    
}
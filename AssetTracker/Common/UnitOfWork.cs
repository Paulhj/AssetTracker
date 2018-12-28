using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Common
{
    public enum Operation
    {
        Create,
        Update,
        Delete
    }

    public class UnitOfWork<TEntity>
    {
        protected IDbContext _context;
        private List<string> _brokenRules;

        public UnitOfWork(IDbContext context)
        {
            _context = context;
            _brokenRules = new List<string>();
        }

        protected int Complete()
        {
            return _context.SaveChanges();
        }

        protected async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        protected virtual void ValidationRules(TEntity item, Operation operation) { }


        protected void Validate(TEntity item, Operation operation)
        {
            //First Clear Rules Collection
            _brokenRules.Clear();

            //Now check any Attributes for validation
            ValidationContext context = new ValidationContext(item, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(item, context, results, true);

            if (isValid == false)
            {
                foreach (var validationResult in results)
                {
                    AddBrokenRule(validationResult.ErrorMessage);
                }
            }

            //Call ValidationRules to run any entity specific rules
            ValidationRules(item, operation);

            //If there are any broken rules throw exception with error messages
            if (_brokenRules.Any())
                throw new EntityException(string.Join("\r\n", _brokenRules));
        }

        protected void AddBrokenRule(string msg)
        {
            _brokenRules.Add(msg);
        }

    }
}

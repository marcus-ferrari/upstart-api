using Upstart.Domain.Utils;
using Upstart.Domain.Validators;

namespace Upstart.Domain.VO
{
    public class AddressVO
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }


        public AddressVO(string _street, string _city, string _state, string _zipcode)
        {
            Street = _street;
            City = _city;
            State = _state;
            ZipCode = _zipcode;

            Validate();
        }

        private void Validate()
        {
            AddressValidator addressValidator = new AddressValidator();
            new ValidateRulesCreateObject<AddressVO>().Validate(addressValidator, this);
        }
    }
}

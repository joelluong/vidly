using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    using System.ComponentModel.DataAnnotations;

    using Vidly.Models;

    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubcribedToNewletter { get; set; }
        
        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
        
        /*[Min18YearsIfAMember]*/
        // comment this attribute in DTO, otherwise we got exception
        public DateTime? Birthdate { get; set; }
    }
}

/*
 * The customer object is part of the domain model of our application
 * It's considered implementation detail which can change frequently
 * as we implement new features in our application
 * And these changes can potentially break existing clients that are
 * dependent on the customer object
 * For example, if we rename or remove a property these can impact the clients
 * that are dependent on that property
 * So basically we need to make the contract of this API as stable as possible
 * It's public contract it should change at a reasonably slower pace than our domain
 * objects.
 *
 * -> To solve this issue we need a different model which we call it Data Transfer Object
 * This DTO is plain data structure and is used to transfer data from the client to the server
 * or vice versa
 *
 * By creating DTO, we reduce the changes of our API breaking as we refactor our domain model
 * And changing DTO can be costly. When we need to change them, we should plan proper strategy
 *
 * To summary, our API is should never receive or return domain objects.
 * Another issue is that by using a domain object here we're opening up security holes in our application
 * A hacker can easily pass additional data into JSON and they will be mapped to our domain object
 * If one of these property should not be updated the hacker can easily bypass this, if we use DTO, we can simply
 * exclude the properties that can be updated
 *
 */
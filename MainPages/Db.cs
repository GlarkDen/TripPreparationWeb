using MainPages.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MainPages
{
    public class Db
    {
        public TripPreparationContext db = new TripPreparationContext();

	    public async Task<List<User>> GetUsersAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            return await db.Users.CountAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await db.Users.SingleOrDefaultAsync(u => u.EmailAddress == email);
        }

	    public async Task<User?> GetUserByPhone(string phone)
	    {
		    return await db.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phone);
	    }

	    public async Task<bool> CheckUserByPhone(string phone, int id)
	    {
		    User? user = await db.Users.SingleOrDefaultAsync(u => u.PhoneNumber == phone && u.UserNumber != id);
		    if (user == null)
			    return true;
		    else
			    return false;
	    }

	    public async Task<bool> CheckUserByEmail(string email, int id)
	    {
		    User? user = await db.Users.SingleOrDefaultAsync(u => u.EmailAddress == email && u.UserNumber != id);
		    if (user == null)
			    return true;
		    else
			    return false;
	    }

	    public async Task<User?> GetUserByIdAsync(int id)
	    {
		    return await db.Users.SingleOrDefaultAsync(u => u.UserNumber == id);
	    }

	    public async Task<int> GetMaxUserIdAsync()
	    {
		    return await db.Users.MaxAsync(u => u.UserNumber);
	    }

        public async Task<int> GetMaxParticipantIdAsync()
        {
            return await db.ParticipantsLists.MaxAsync(u => u.ParticipantNumber);
        }

        public async Task<int> GetMaxTripIdAsync()
        {
            return await db.TripsLists.MaxAsync(u => u.TripNumber);
        }

        public async Task<User> AddUserAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> SearchUserByFullNameAsync(string fullname)
        {
            string[] data = fullname.Split();

            if (data.Length == 1)
            {
                if (data[0] == "")
                    return new List<User>();

			    return await db.Users.Where(u => u.Surname.Contains(data[0])).ToListAsync();
            }
            else if (data.Length == 2)
                return await db.Users.Where(u => u.Surname.Contains(data[0]) && u.Name.Contains(data[1])).ToListAsync();
            else if (data.Length == 3)
                return await db.Users.Where(u => u.Surname.Contains(data[0]) && u.Name.Contains(data[1]) && u.Patronymic.Contains(data[2])).ToListAsync();
            else
                return new List<User>();
        }

	    public async Task<List<TripsList>> GetTripsAsync()
	    {
		    return await db.TripsLists.ToListAsync();
	    }

	    public async Task<List<Post>> GetPostsAsync()
	    {
		    return await db.Posts.ToListAsync();
	    }

	    public async Task<Post?> GetPostByIdAsync(int id)
	    {
		    return await db.Posts.SingleOrDefaultAsync(u => u.PostNumber == id);
	    }

        public async Task<List<EquipmentList>> GetEquipmentByTripIdAsync(int tripId)
        {
            return await db.EquipmentLists.Where(e => e.TripNumber == tripId).ToListAsync();
        }

        public async Task<List<ProductsList>> GetProductByTripIdAsync(int tripId)
        {
            return await db.ProductsLists.Where(e => e.TripNumber == tripId).ToListAsync();
        }

        public async Task<List<Food>> GetFoodByTripIdAsync(int tripId)
        {
            return await db.Foods.Where(e => e.TripNumber == tripId).ToListAsync();
        }

        public async Task<TripsList?> GetTripByIdAsync(int id)
	    {
		    return await db.TripsLists.SingleOrDefaultAsync(u => u.TripNumber == id);
	    }

	    public async Task<User> UpdateUserAsync(User user)
        {
            db.Users.Update(user);
            await db.SaveChangesAsync();
            return user;
        }

	    public async Task<ParticipantsList> UpdateParticipantAsync(ParticipantsList participant)
	    {
		    db.ParticipantsLists.Update(participant);
		    await db.SaveChangesAsync();
		    return participant;
	    }

	    public async Task<User> DeleteUserAsync(User user)
        {
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<TripsList> DeleteTripAsync(TripsList trip)
	    {
		    db.TripsLists.Remove(trip);
		    await db.SaveChangesAsync();
		    return trip;
	    }

        public async Task<List<TripType>> GetTripTypesAsync()
        {
            return await db.TripTypes.ToListAsync();
        }

        public async Task<TripsList> AddTripAsync(TripsList trip)
        {
            db.TripsLists.Add(trip);
            await db.SaveChangesAsync();
            return trip;
        }

        public async Task<TripType?> GetTripTypeByIdAsync(int id)
        {
            return await db.TripTypes.SingleOrDefaultAsync(u => u.TripTypeNumber == id);
        }

        public async Task<ParticipantsList> AddParticipantAsync(ParticipantsList participant)
        {
            db.ParticipantsLists.Add(participant);
            await db.SaveChangesAsync();
            return participant;
        }

	    public async Task<ParticipantsList> DeleteParticipantAsync(ParticipantsList participant)
	    {
		    db.ParticipantsLists.Remove(participant);
		    await db.SaveChangesAsync();
		    return participant;
	    }

	    public async Task<ParticipantsList?> GetParticipantByUserAndTripAsync(int userId, int tripId)
	    {
		    return await db.ParticipantsLists.FirstOrDefaultAsync(p => p.UserNumber == userId && p.TripNumber == tripId);
	    }

        public async Task<int> GetTripParticipantCountAsync(int tripId)
        {
            return await db.ParticipantsLists.CountAsync(p => p.TripNumber == tripId);
        }

        public async Task<TripsList> UpdateTripAsync(TripsList trip)
        {
            db.TripsLists.Update(trip);
            await db.SaveChangesAsync();
            return trip;
        }

        public async Task<List<ParticipantsList>> GetParticipantsByTripAsync(int tripId)
        {
            return await db.ParticipantsLists.Where(p => p.TripNumber == tripId).ToListAsync();
        }

        public async Task<ParticipantsList?> GetParticipantsByIdAsync(int id)
        {
            return await db.ParticipantsLists.FirstOrDefaultAsync(p => p.ParticipantNumber == id);
        }
    }
}

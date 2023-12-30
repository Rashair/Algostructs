namespace Queues;

public class AnimalShelter : IAnimalShelter
{
    private readonly LinkedList<ShelterAnimalContainer<Cat>> _cats = [];
    private readonly LinkedList<ShelterAnimalContainer<Dog>> _dogs = [];

    public void Enqueue(Animal animal)
    {
        var addedDate = DateTimeOffset.Now;
        switch (animal)
        {
            case Cat cat:
                _cats.AddLast(new ShelterAnimalContainer<Cat>(cat, addedDate));
                return;

            case Dog dog:
                _dogs.AddLast(new ShelterAnimalContainer<Dog>(dog, addedDate));
                return;

            default:
                throw new ArgumentException($"Animal {animal.GetType()} should be either a cat or a dog");
        }
    }

    public Animal DequeueAny()
    {
        if (_cats.Count == 0 && _dogs.Count == 0)
        {
            throw new InvalidOperationException("Shelter is empty.");
        }

        if (_cats.Count == 0)
        {
            return DequeueDog();
        }

        if (_dogs.Count == 0)
        {
            return DequeueCat();
        }

        var oldestCat = _cats.First!.Value;
        var oldestDog = _dogs.First!.Value;
        return oldestCat.AddedDate < oldestDog.AddedDate ? DequeueCat() : DequeueDog();
    }

    public Cat DequeueCat()
    {
        if (_cats.Count == 0)
        {
            throw new InvalidOperationException("No cats in the shelter.");
        }

        var cat = _cats.First!.Value.Animal;
        _cats.RemoveFirst();
        return cat;
    }

    public Dog DequeueDog()
    {
        if (_dogs.Count == 0)
        {
            throw new InvalidOperationException("No dogs in the shelter.");
        }

        var dog = _dogs.First!.Value.Animal;
        _dogs.RemoveFirst();
        return dog;
    }

    private class ShelterAnimalContainer<TAnimal>
        where TAnimal : Animal
    {
        public TAnimal Animal { get; }
        public DateTimeOffset AddedDate { get; }

        public ShelterAnimalContainer(TAnimal animal, DateTimeOffset addedDate)
        {
            Animal = animal;
            AddedDate = addedDate;
        }
    }
}

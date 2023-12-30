using NUnit.Framework;

namespace Queues.Tests;

[TestFixture]
[TestOf(typeof(AnimalShelter))]
public class AnimalShelterTests
{
    private AnimalShelter _shelter;

    [SetUp]
    public void Setup()
    {
        _shelter = new();
    }

    [Test]
    public void Enqueue_Cat_ShouldAddCatToShelter()
    {
        var cat = new Cat();
        _shelter.Enqueue(cat);
        Assert.That(() => _shelter.DequeueCat(), Throws.Nothing);
    }

    [Test]
    public void Enqueue_Dog_ShouldAddDogToShelter()
    {
        var dog = new Dog();
        _shelter.Enqueue(dog);
        Assert.That(() => _shelter.DequeueDog(), Throws.Nothing);
    }

    [Test]
    public void Enqueue_InvalidAnimal_ShouldThrowArgumentException()
    {
        var shark = new Shark();
        Assert.That(() => _shelter.Enqueue(shark), Throws.ArgumentException);
    }
    private class Shark : Animal;

    [Test]
    public void DequeueAny_EmptyShelter_ShouldThrowInvalidOperationException()
    {
        Assert.That(() => _shelter.DequeueAny(), Throws.InvalidOperationException);
    }

    [Test]
    public void DequeueAny_CatsAndDogs_ShouldDequeueOldestAnimal()
    {
        var cat = new Cat();
        var dog = new Dog();
        _shelter.Enqueue(cat);
        System.Threading.Thread.Sleep(10); // Ensure different added times
        _shelter.Enqueue(dog);
        Assert.That(_shelter.DequeueAny(), Is.EqualTo(cat));
    }

    [Test]
    public void DequeueCat_NoCats_ShouldThrowInvalidOperationException()
    {
        Assert.That(() => _shelter.DequeueCat(), Throws.InvalidOperationException);
    }

    [Test]
    public void DequeueDog_NoDogs_ShouldThrowInvalidOperationException()
    {
        Assert.That(() => _shelter.DequeueDog(), Throws.InvalidOperationException);
    }

    [Test]
    public void DequeueAny_MultipleCatsAndDogs_ShouldDequeueOldestAnimal()
    {
        var initialCat = new Cat();
        _shelter.Enqueue(initialCat);
        System.Threading.Thread.Sleep(9); // Ensure different added times
        for(int i = 0; i < 100; i++)
        {
            _shelter.Enqueue(new Dog());
            System.Threading.Thread.Sleep(10); // Ensure different added times
            _shelter.Enqueue(new Cat());
        }

        Assert.That(_shelter.DequeueAny(), Is.EqualTo(initialCat));
    }
}

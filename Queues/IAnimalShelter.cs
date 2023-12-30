namespace Queues;

public abstract class Animal;
public class Dog : Animal;
public class Cat : Animal;

public interface IAnimalShelter
{
    void Enqueue(Animal animal);
    Animal DequeueAny();
    Cat DequeueCat();
    Dog DequeueDog();
}

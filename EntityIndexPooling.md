# Entity Index pooling

## Entities

Entities should be composed of a generation index and an index.
When referencing an entity, it should point to an index and a generation both at once.

This solves the issue of pointing to an entity that has been destroyed and its index reused.

## Entity index reservation

When calling the spawn method, the index is added to the list of reserved indices and once the logic has finished running, reserved indices will be added to the world.



# State event system

States are represented by uint values. When a state is registered the default value will be used.

## State changes

A user will have to request a state change programmatically through the world commands. 

State changes will ideally be applied to the next frame, ensuring every processors/systems work with the same data at the same time.

To make sure those states are updated, they will be treated through the world commands object.
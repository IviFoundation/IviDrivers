# Using Strings as Repeated Capability Selectors

This document describes a tested way to use a string as a repeated capability selector in an IVI driver where there is a complex mix of nested collection style, selector style, and parameter style repeated capabilities that need to optionally express multiple selections.

These syntaxes are not necessary for repeated capabilities that use collection indexes that return only one repeated capability instance. For most applications a complex syntax such as this is not necessary.

## Recommended Formal Syntax for String Repeated Capability Selectors

The following describes a formal syntax for repeated capability selectors.

A syntactically valid repeated capability selector consists of zero or more *repeated capability path segments* separated by colons (:). White space around colons is ignored.

A repeated capability path segment consists of one or more *repeated capability list elements*, separated by commas (,). White space after commas is ignored. A repeated capability path segment may be enclosed in square brackets (\[\]).

A repeated capability list element consists of a *repeated capability token* or a repeated capability range.

A repeated capability range consists of two repeated capability tokens separated by a hyphen (-).

The order of precedence of operators is: square brackets (\[\]), hyphen (-), colon (:), and comma (,). Each operator is evaluated from left to right.

A repeated capability token is a physical repeated capability identifer or a virtual repeated capability identifier.

A syntactically valid physical or virtual repeated capability identifier consists of one or more of the following characters: a-z, A-Z, 0-9, !, and \_.

## Representing a Set of Instances with a String

To specify a set of repeated capability instances in a string, repeated capability selectors may use *repeated capability ranges* and *repeated capability lists*. A repeated capability range designates a sequence of repeated capabilities (for instance from "1-5"). A repeated capability list designates a specific set of repeated capabilities (for instance "1,2,3,5").

The syntax chosen by a driver to implement repeated capability ranges and lists is defined by the driver.

## Representing Nested Repeated Capabilities with a String

When using the parameter/selector approach for specifying nested repeated capabilities with a string, all the information needed to navigate the hierarchy is represented in a single selector string. The repeated capability identifiers at each level in the hierarchy are concatenated using colons as separators. Each identifier may be physical or virtual. The identifier for the repeated capability instance at the top level of the hierarchy appears first in the string. White space around colons should be ignored. Such selectors are called *hierarchical repeated capability selectors*.

As an example, consider a power supply with four output channels, each of which has two configurable external triggers. To configure a specific trigger with a parameter style repeated capabilty string, the user specifies the output channel and the trigger. A function call might look like the following:

    ConfigureExternalTrigger("Out1:Trig1", Source, Level);

where "Out1:Trig1" represents a specific trigger (Trig1) for a specific output (Out1), and where "Trig1" and "Out1" are identifiers for the respective repeated capability instances.

## Mixing Nested Repeated Capabilities with Sets of Instances

Selectors for nested repeated capabilities may contain lists or ranges at any level of the hierarchy. Mixing hierarchy with lists or ranges requires using the colon (:), comma (,), and hyphen (-) operators in the same selector. The interpretation of such a selector can be ambiguous unless the order of precedence is clear. The use of square brackets ([]) may be required to overide the precedence of the colon (:) and comma (,) operators.

The order of precedence is square brackets ([]), hyphen (-), colon (:), and comma (,). Each operator is evaluated from left to right.

For example, `"a1:b2:\[c5,c7\]"` expands to the following list:

    "a1:b2:c5, a1:b2:c7"

whereas "a1-a3:b2:c5,c7" evaluates to:

    "a1:b2:c5, a1:b2:c7, a2:b2:c5, a2:b2:c7, a3:b2:c5, a3:b2:c7"


Note: Although both examples are syntactically correct, only the first example is valid. The second in invalid because all repeated capability identifiers within a list of repeated capability identifiers must have the same level of nesting after expansion.

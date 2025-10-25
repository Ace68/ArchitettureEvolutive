# EventStorming Software Design Workshop

## What It Is

**EventStorming Software Design** is a collaborative workshop method created by **Alberto Brandolini**.  
It is a *visual and participatory* approach for exploring, understanding, and modeling a complex software domain — starting from the events that occur within the system (*domain events*).

## Objectives

- **Knowledge Alignment**: Surface and align the shared understanding among business stakeholders, product owners, developers, UX designers, etc.  
- **Domain Discovery**: Make processes, flows, rules, and pain points visible.  
- **Collaborative Design**: Reach a software representation that closely matches the business reality.  
- **Identification of Bounded Contexts**: Prepare the foundation for a Domain-Driven Design (DDD)-based architecture.  

## Typical Workshop Structure

1. **Domain Events**  
   - Participants write significant system events that *have happened* (e.g., “Order Created”, “Payment Received”, “Shipment Started”) on orange sticky notes.  
   - Events are arranged in chronological order on a large visual surface (wall, whiteboard, or canvas).  

2. **Commands**  
   - Identify the *stimuli* that caused those events (e.g., “Create Order”, “Pay Order”, “Ship Goods”).  
   - Usually represented with blue sticky notes.  

3. **Aggregates / Entities**  
   - Discuss which domain entities handle those commands and generate the events (e.g., “Order” as the aggregate root).  
   - Represented with yellow sticky notes.  

4. **Policies / Process Managers**  
   - Introduce automatic rules or processes that react to events and generate new commands.  
   - Represented with purple sticky notes.  

5. **Read Models and UI**  
   - Explore the read models needed and the user interfaces that consume them.  
   - Represented with green sticky notes.  

6. **Bounded Contexts**  
   - Identify coherent and autonomous subsystems (e.g., “Order Management”, “Payments”, “Shipping”).  
   - This phase is crucial for defining the software architecture.  

7. **Hot Spots / Open Issues**  
   - Highlight areas of uncertainty or conflict using red sticky notes — to guide future discussions.  

## Workshop Outputs

- A visual map of domain events and processes.  
- A shared understanding between the technical and business teams.  
- Early drafts of a DDD-oriented software design (aggregates, bounded contexts, event flows).  
- A list of risk areas or topics requiring further clarification.  

## Key Characteristics

- **Collaborative**: Everyone contributes — not just developers.  
- **Visual**: Everything is displayed on a large surface using colored sticky notes.  
- **Iterative**: Start from the high-level *Big Picture* and then move into detailed *Design-Level* discussions.  
- **Empirical**: The goal is exploration and adaptation, not immediate perfection.  

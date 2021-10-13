using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// An Array containing all actors in a scene
        /// </summary>
        private Actor[] _actors;

        public Scene()
        {
            _actors = new Actor[0];
        }

        /// <summary>
        /// Calls Start for every actor in the scene
        /// </summary>
        public virtual void Start()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Start();
        }

        /// <summary>
        /// Updates the actors in the scene
        /// Calls start for an actor if it hasn't already been called
        /// </summary>
        public virtual void Update()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update();

                //Checks for collision
                for (int j = 0; j < _actors.Length; j++)
                        if (_actors[i].Position == _actors[j].Position && i != j)
                            _actors[i].OnCollision(_actors[j]);
            }
                

        }

        /// <summary>
        /// Draws every actor in the scene
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        public virtual void End()
        {

        }

        /// <summary>
        /// Adds an actor to the scenes list of actors
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Actor actor)
        {
            //Creates a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _actors.Length; i++)
                tempArray[i] = _actors[i];
            //Adds the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Merges the arrays
            _actors = tempArray;
        }

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>If the removal was successful</returns>
        public bool RemoveActor(Actor actor)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i] != actor)
                {
                    tempArray[i] = _actors[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }  
            
            //Merges the arrays
            if(actorRemoved)
            _actors = tempArray;

            return actorRemoved;
        }
    }
}

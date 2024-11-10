using System;
using System.Collections.Generic;

namespace FlowFreeSolver
{
    /// Implementación de una cola de prioridad basada en un heap binario.
    /// <typeparam name="TElement">Tipo de los elementos.</typeparam>
    /// <typeparam name="TPriority">Tipo de la prioridad.</typeparam>
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {
        private List<(TElement Element, TPriority Priority)> heap = new List<(TElement, TPriority)>();

        public int Count => heap.Count;

        public void Enqueue(TElement element, TPriority priority)
        {
            heap.Add((element, priority));
            HeapifyUp(heap.Count - 1);
        }

        public TElement Dequeue()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("La cola está vacía.");

            var element = heap[0].Element;
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return element;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int padre = (index - 1) / 2;
                if (heap[index].Priority.CompareTo(heap[padre].Priority) >= 0)
                    break;

                var temp = heap[index];
                heap[index] = heap[padre];
                heap[padre] = temp;
                index = padre;
            }
        }

        private void HeapifyDown(int index)
        {
            int count = heap.Count;
            while (index < count)
            {
                int hijoIzq = 2 * index + 1;
                int hijoDer = 2 * index + 2;
                int menor = index;

                if (hijoIzq < count && heap[hijoIzq].Priority.CompareTo(heap[menor].Priority) < 0)
                    menor = hijoIzq;
                if (hijoDer < count && heap[hijoDer].Priority.CompareTo(heap[menor].Priority) < 0)
                    menor = hijoDer;

                if (menor == index)
                    break;

                var temp = heap[index];
                heap[index] = heap[menor];
                heap[menor] = temp;
                index = menor;
            }
        }
    }
}

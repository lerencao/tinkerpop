package com.tinkerpop.gremlin.process.graph.strategy;

import com.tinkerpop.gremlin.process.Step;
import com.tinkerpop.gremlin.process.Traversal;
import com.tinkerpop.gremlin.process.TraversalStrategy;
import com.tinkerpop.gremlin.process.graph.marker.Comparing;
import com.tinkerpop.gremlin.process.TraversalEngine;
import com.tinkerpop.gremlin.process.util.TraversalHelper;

/**
 * @author Marko A. Rodriguez (http://markorodriguez.com)
 */
public class ComparingReductionStrategy extends AbstractTraversalStrategy implements TraversalStrategy.NoDependencies {

    private static final ComparingReductionStrategy INSTANCE = new ComparingReductionStrategy();

    private ComparingReductionStrategy() {
    }

    @Override
    public void apply(final Traversal<?, ?> traversal, final TraversalEngine engine) {
        if(engine.equals(TraversalEngine.STANDARD))
            return;

        if (TraversalHelper.hasStepOfAssignableClass(Comparing.class, traversal)) {
            final Step endStep = TraversalHelper.getEnd(traversal);
            TraversalHelper.getStepsOfAssignableClass(Comparing.class, traversal)
                    .stream()
                    .filter(step -> step != endStep)
                    .forEach(step -> TraversalHelper.removeStep(step, traversal));
        }
    }

    public static ComparingReductionStrategy instance() {
        return INSTANCE;
    }
}
